/*
  FS Platform Controller 
*/

// Pin definitions
const int pinPotentiometer = A0;
const int pinDirection = 2;
const int pinPWM = 3;

// System constants
const int upSpeeds[6]   = {0, 80, 100, 110, 125, 250};
const int downSpeeds[6] = {0, 80, 100, 110, 125, 250};
const int serialBaudRate = 115200;
const int separationCharacter = ',';

// Platform calibration
const int maxAngle = 20;
const int potZero = 407;
const int potDeltaMax = 134;

// System variables
int targetAngle = 0;
int targetPot = potZero;
int currentPot;
int pwmValue = 0;
bool directionValue;
int deltaPot = 0;
String command;
bool testMode = false;

void setup() {
  // Set pin modes
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(pinPWM, OUTPUT);
  pinMode(pinDirection, OUTPUT);
  
  Serial.begin(115200);
}

void loop() {
  // Read command
  readCommand();
  targetPot = map(targetAngle, -20, 20, 236, 588);
  if (targetPot > 588) targetPot = 588;
  if (targetPot < 236) targetPot = 236;
  currentPot = analogRead(pinPotentiometer);

  // Set direction
  bool newDirectionValue = (targetPot <= currentPot);
  if (newDirectionValue != directionValue) {
    if (!testMode) digitalWrite(pinDirection, newDirectionValue);
    directionValue = newDirectionValue;
  }

  // Set PWM
  deltaPot = min(5, abs(currentPot - targetPot) / 8.8);
  int newPWMValue = upSpeeds[deltaPot];
  if (newPWMValue != pwmValue) {
    if (!testMode) analogWrite(pinPWM, newPWMValue);
    pwmValue = newPWMValue;
  }
}

void readCommand() {
  command = Serial.readStringUntil(separationCharacter);

  if (command == "on") {
    digitalWrite(LED_BUILTIN, HIGH);
    return;
  }

  if (command == "off") {
    digitalWrite(LED_BUILTIN, LOW);
    return;
  }

  if (command.startsWith("D")) {
    targetAngle = command.substring(1).toInt();
    return;
  }

  if (command == "getTarget") {
    Serial.println("Target angle: " + String(targetAngle));
    return;
  }

  if (command == "getTargetPot") {
    Serial.println("Target potentiometer: " + String(targetPot));
    return;
  }

  if (command == "getPWM") {
    Serial.println("Current PWM: " + String(pwmValue));
    return;
  }

  if (command == "getDeltaPot") {
    Serial.println("Current deltaPot: " + String(deltaPot));
    return;
  }

  if (command == "getCurrentPot") {
    int cPot = analogRead(pinPotentiometer);
    Serial.println("Current Pot: " + String(cPot));
    return;
  }

  if (command == "disableTestMode") {
    testMode = false;
    return;
  }

  if (command == "pulsePositive") {
    digitalWrite(pinDirection, HIGH);
    analogWrite(pinPWM, 100);
    delay(1000);
    analogWrite(pinPWM, 0);
    return;
  }

  if (command == "pulseNegative") {
    digitalWrite(pinDirection, LOW);
    analogWrite(pinPWM, 100);
    delay(1000);
    analogWrite(pinPWM, 0);
    return;
  }
}

