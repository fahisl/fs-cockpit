void setup() {
    Serial.begin(115200);
    pinMode(LED_BUILTIN, OUTPUT);
    digitalWrite(LED_BUILTIN, LOW);
    Serial.println("Setup complete");
}

const char separationCharacter = ';';
int target = 0;
int targetAngle = 0;

void loop() {
    readCommand();
    Serial.println(targetAngle);
}

void readCommand() {
    String command = Serial.readStringUntil(separationCharacter);

    if (command.startsWith("D")) {
      targetAngle = command.substring(1).toInt();
      targetAngle /= 100;
      return;
    }
}
