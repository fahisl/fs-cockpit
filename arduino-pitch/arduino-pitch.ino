/*
 Skysim Bank Program
 Revision 130422
 Tested: 2014/11/22
 Production date: 2014/11/22
 FSX positive angle = nose down
 Actuator LOW = nose down

Designed for Arduino Uno

Wiring requirements:
  Potentiometer reference = blue
  A0 = potentiometer wiper
  2 = actuator direction
  3 = PWM to actuator
  4 = Button to commit custom zero position
*/

#include <EEPROM.h>

//Pin Definitions
const int potPin = A0;        // Potentiometer pin
const int pinDir = 2;         // Direction Pin
const int pinPwm = 3;         // PWM Pin
const int buttonCustomZero = 4;

// App Configuration
//const int upSpeedTable[6]   = {0, 150, 160, 170, 180, 250};  // Actuator speed function
const int upSpeedTable[6]   = {0, 80, 170, 180, 200, 250};  // Actuator speed function
const int downSpeedTable[6] = {0, 80, 100, 110, 150, 250};  // Actuator speed function
const char commandCharacter = 'A';  // All valid commands are expected to start with this character

// Non-configurable parameters
const int serialBaudRate = 9600;
const char separationCharacter = ','; // All valid commands end with this character
/*const double eepromFactor = 2.24;
const int potDeltaMax = 290;  // 571 by default
const int potDeltaMin = -63;  // 208 by default
const int potZeroDefault = 509;*/

// Function Variables
boolean resetPlatform = true;  // True if the platform needs to be zeroed
boolean validCommand = false;
String commandString;  // A string parsed and prepared for processing
char inChar;                // Temp character
int desiredAngle;       // Angle desired by Flight Simulator
const int potZero = 400;  //potZeroDefault; default 509
const int potMax = 597;
const int potMin = 219;  // 20 degrees nose up
int potDesired = potZero;
int potCurrent;


void setup() {
  Serial.begin(serialBaudRate); 
  
  //Serial.println("Reserving space for commands...");
  commandString.reserve(200);
  
  // PIN DECLERATION
  //Serial.println("Assigning pins...");
  pinMode(pinPwm, OUTPUT);
  pinMode(pinDir, OUTPUT);
  pinMode(buttonCustomZero, INPUT);
  
  //INITIALIZING THE BOARD
  /*Serial.println("INITIALIZING ARDUINO MOTION PLATFORM Mark 2");
  Serial.println("PARAMETERS INITIALIZATION");
  Serial.print("--> <AUTOLEVEL SET> to: ");
  Serial.println(resetPlatform);*/
  
  // initial target angle is zero
  potDesired = potZero;
  //Serial.println("Adjusting platform angle to zero...");
  
  while (resetPlatform) {
    potCurrent = analogRead(potPin);
    //Serial.print("Current Pot: "); Serial.println(potCurrent);
    
    // verify platform starts out within 1 degree of zero level
    if (abs(potZero-potCurrent) <= 13) {
      //Serial.println("Zero angle achieved. Braking.");
      resetPlatform = false;
      applyBrake();
    }
    else {
      runActuator();
    }
    
    //DEBUG
    delay(50);
  }
}

void loop() {
  potCurrent = analogRead(potPin);
  
  // Print out current state
  //Serial.print("Current Pot: "); Serial.println(potCurrent);
  //Serial.print("Desired Pot: "); Serial.println(potDesired);
  
  // Safety Brake
  if (!isValidPot(potCurrent)) { applyBrake(); }
  if (validCommand) { getDesiredAngle(); validCommand = false; }
  
  runActuator();
  
  // Custom zero routine
  // if (digitalRead(buttonCustomZero) == HIGH) { customZero(); }
  
  delay(50);
}

void serialEvent() {
  while (Serial.available() > 0) {
    inChar = (char) Serial.read();
    switch (inChar) {
      case commandCharacter:
        //Serial.println("Command character received");
        commandString = (String) inChar;
        validCommand = false;
        break;
      case separationCharacter:
        validCommand = true;
        break;
      default:
        commandString += inChar;
    }
  }
}
 
void getDesiredAngle() {
  commandString.setCharAt(0,' ');
  commandString.trim();
  char carray[10];
  commandString.toCharArray(carray, sizeof(carray));
  commandString = '0'; //clears variable for new input
  validCommand = false;
  desiredAngle = atoi(carray);
  potDesired = map(desiredAngle, -20, 5, potMin, potMax);
  potDesired = constrain(potDesired, potMin, potMax);
}

boolean isValidPot(int potValue) {
  if ((potValue > potMax) || (potValue < potMin)) { return false; }
  else { return true; }
}

void runActuator() {
  int potDelta = abs(potCurrent - potDesired) / 12.5;  
  if (potDelta > 5) { potDelta = 5; }
  
  //Serial.print("Pot delta "); Serial.println(potDelta);

  if (potDesired > potCurrent) { noseDown(potDelta); }   // PWM Low
  else if (potDesired < potCurrent) { noseUp(potDelta); }  // PWM HIGH
  //else { applyBrake(); }
}

void noseDown(int pwmSpeed) {
  //Serial.print("PWM low "); Serial.println(downSpeedTable[pwmSpeed]);
  digitalWrite(pinDir, LOW);
  analogWrite(pinPwm, downSpeedTable[pwmSpeed]);
}

void noseUp(int pwmSpeed) {
  //Serial.print("PWM high "); Serial.println(upSpeedTable[pwmSpeed]);
  digitalWrite(pinDir, HIGH);
  analogWrite(pinPwm, upSpeedTable[pwmSpeed]);
}

void applyBrake() {
  //Serial.println("Brakes");
  analogWrite(pinPwm, 0);
  digitalWrite(pinDir, LOW);
}

/*void customZero() {
  int customPot = analogRead(potPin) / eepromFactor;  
  
  // Start by clearing EEPROM
  clearEeprom();
  
  // Save the custom zero to EEPROM
  Serial.println("Saving to EEPROM");
  EEPROM.write(0, (customPot));
  
  Serial.println("Saved");
  Serial.println("Custom zero adjustment is complete. Resetting board...");
  asm volatile ("jmp 0");
}

void clearEeprom() {
  Serial.println("Clearing EEPROM");
  for (int i = 0; i < 512; i++) { EEPROM.write(i, 0); }
}*/
