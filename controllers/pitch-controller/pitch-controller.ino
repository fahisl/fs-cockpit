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
const int separationCharacter = ',';

// Platform calibration
const int potZero = 467;
const int maxPot = 543;
const int minPot = 281;
const int mapperMinAngle = -10;
const int mapperMinPot = 391;
const int mapperMaxAngle = 10;
const int mapperMaxPot = 543;

// System variables
double potPerDegree = (mapperMaxPot - potZero) / mapperMaxAngle;
int targetAngle = 0;
int targetPot = potZero;
int currentPot;
int deltaPot = 0;
int pwmValue = 0;
bool directionValue;
bool testMode = false;
String command;

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

  // Potentiometer values
  targetPot = map(targetAngle, mapperMinAngle, mapperMaxAngle, mapperMinPot, mapperMaxPot);
  targetPot = constrain(targetPot, minPot, maxPot);
  currentPot = analogRead(pinPotentiometer);  

  // Set direction
  bool newDirectionValue = (targetPot <= currentPot);
  if (newDirectionValue != directionValue) {
    if (!testMode) digitalWrite(pinDirection, newDirectionValue);
    directionValue = newDirectionValue;
  }

  // Set PWM
  deltaPot = min(5, abs(currentPot - targetPot) / potPerDegree);
  int newPWMValue = upSpeeds[deltaPot];
  if (newPWMValue != pwmValue) {
    if (!testMode) analogWrite(pinPWM, newPWMValue);
    pwmValue = newPWMValue;
  }
}

void pulse(bool direction, int duration, int pwm) {
  digitalWrite(pinDirection, direction);
  analogWrite(pinPWM, pwm);
  delay(duration);
  analogWrite(pinPWM, 0);
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
    pulse(HIGH, 1000, 100);
    return;
  }

  if (command == "pulseNegative") {
    pulse(LOW, 1000, 100);
    return;
  }
}

