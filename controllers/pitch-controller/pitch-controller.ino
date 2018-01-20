/*
  FS Platform Controller 
*/

// Pin definitions
const int pinPotentiometer = A0;
const int pinDirection = 2;
const int pinPWM = 3;

// System constants
const int separationCharacter = ',';

// Platform calibration
const double maxPWMDegrees = 5;
const int maxPWM = 255;
const int minPWM = 0;
const double potZero = 467;
const double maxPot = 543;
const double minPot = 281;
const double realDegreesFactor = 100;
const double mapperMinAngle = -10;
const double mapperMinPot = 391;
const double mapperMaxAngle = 10;
const double mapperMaxPot = 543;

// System variables
double potPerDegree = (mapperMaxPot - potZero) / mapperMaxAngle;
double pwmFactor = (maxPWM / maxPWMDegrees) / potPerDegree;
double targetAngle = 0;
double targetPot = potZero;
int currentPot;
int deltaPot = 0;
int nextPWMValue = 0;
int pwmValue = 0;
bool nextDirectionValue;
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
  readCommand();
  setTarget();
  setDirection();
  setPWM();
}

void setTarget() {
  targetPot = dmap(targetAngle, mapperMinAngle, mapperMaxAngle, mapperMinPot, mapperMaxPot);
  targetPot = constrain(targetPot, minPot, maxPot);
  currentPot = testMode ? 0 : analogRead(pinPotentiometer);
  deltaPot = abs(currentPot - targetPot);
}

void setDirection() {
  nextDirectionValue = (targetPot <= currentPot);
  if (nextDirectionValue != directionValue) {
    if (!testMode) digitalWrite(pinDirection, nextDirectionValue);
    directionValue = nextDirectionValue;
  }
}

void setPWM() {
  nextPWMValue = pwmFactor * deltaPot;
  nextPWMValue = constrain(nextPWMValue, 0, maxPWM);

  // This block prevents using PWM that is too low to move the platform
  if (nextPWMValue < minPWM) {
    nextPWMValue = 0;
  }

  if (nextPWMValue != pwmValue) {
    if (!testMode) analogWrite(pinPWM, nextPWMValue);
    pwmValue = nextPWMValue;
  }
}

void pulse(bool direction, int duration, int pwm) {
  digitalWrite(pinDirection, direction);
  analogWrite(pinPWM, pwm);
  delay(duration);
  analogWrite(pinPWM, 0);
}

double dmap(double x, double in_min, double in_max, double out_min, double out_max) {
  return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
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
    targetAngle /= 100;
    return;
  }

  if (command == "getTarget") {
    Serial.println("Target angle: " + String(targetAngle));
    return;
  }

  if (command == "getTargetPot") {
    Serial.println("Target pot: " + String(targetPot));
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

