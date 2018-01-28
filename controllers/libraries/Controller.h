#ifndef Controller_h
#define Controller_h

class Controller {
  public:
    String boardId;
    const int separationCharacter = ',';
    Controller(String id);
    void configPins(int pot, int dir, int pwm);
    void configMapping(double _minAngle, double _minPot, double _maxAngle, double _maxPot, double _potZero);
    void configPWM(double _max, double _min, double _maxDegrees);
    void configLimits(double _min, double _max);
    void main();

  private:
    int pinPotentiometer;
    int pinDirection;
    int pinPWM;
    double mapperMinAngle;
    double mapperMinPot;
    double mapperMaxAngle;
    double mapperMaxPot;
    double potZero;
    double potPerDegree;
    double pwmFactor;
    int maxPWM;
    int minPWM;
    double maxPWMDegrees;
    double realDegreesFactor = 100;
    double maxPot;
    double minPot;

    String command;
    double targetAngle = 0;
    double targetPot;

    int currentPot;
    int deltaPot = 0;

    int nextPWMValue = 0;
    int pwmValue = 0;
    bool nextDirectionValue;
    bool directionValue;

    bool testMode = false;

    void setTarget();
    void setPWM();
    void setDirection();
    void readCommand();
    void pulse(bool direction, int duration, int pwm);
};

#endif

Controller::Controller(String id) {
  boardId = id;
}

void Controller::configPins(int pot, int dir, int pwm) {
  pinPotentiometer = pot;
  pinDirection = dir;
  pinPWM = pwm;
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(pinPWM, OUTPUT);
  pinMode(pinDirection, OUTPUT);
}

void Controller::configMapping(double _minAngle, double _minPot, double _maxAngle, double _maxPot, double _potZero) {
  mapperMinAngle = _minAngle;
  mapperMinPot = _minPot;
  mapperMaxAngle = _maxAngle;
  mapperMaxPot = _maxPot;
  potZero = _potZero;
  targetPot = potZero;
  potPerDegree = (mapperMaxPot - potZero) / mapperMaxAngle;
}

void Controller::configPWM(double _max, double _min, double _maxDegrees) {
  maxPWM = _max;
  minPWM = _min;
  maxPWMDegrees = _maxDegrees;
  pwmFactor = (maxPWM / maxPWMDegrees) / potPerDegree;
}

void Controller::configLimits(double _min, double _max) {
  minPot = _min;
  maxPot = _max;
}

void Controller::main() {
  readCommand();
  setTarget();
  setDirection();
  setPWM();
}

void Controller::setTarget() {
  targetPot = (targetAngle - mapperMinAngle) * (mapperMaxPot - mapperMinPot) / (mapperMaxAngle - mapperMinAngle) + mapperMinPot;
  targetPot = constrain(targetPot, minPot, maxPot);
  currentPot = testMode ? potZero : analogRead(pinPotentiometer);
  deltaPot = abs(currentPot - targetPot);
}

void Controller::setDirection() {
  nextDirectionValue = (targetPot <= currentPot);
  if (nextDirectionValue != directionValue) {
    if (!testMode) digitalWrite(pinDirection, nextDirectionValue);
    directionValue = nextDirectionValue;
  }
}

void Controller::setPWM() {
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

void Controller::pulse(bool direction, int duration, int pwm) {
  digitalWrite(pinDirection, direction);
  analogWrite(pinPWM, pwm);
  delay(duration);
  analogWrite(pinPWM, 0);
}

void Controller::readCommand() {
  command = Serial.readStringUntil(separationCharacter);

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

  if (command == "ident") {
    Serial.println(boardId);
    return;
  }

  if (command == "on") {
    digitalWrite(LED_BUILTIN, HIGH);
    return;
  }

  if (command == "off") {
    digitalWrite(LED_BUILTIN, LOW);
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
