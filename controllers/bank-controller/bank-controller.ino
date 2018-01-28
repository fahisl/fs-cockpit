// FS Platform Controller 

#include "Controller.h"

Controller controller("c5b6d4a4-09f2-4a3f-ae90-2a2b3c24d842");

void setup() {
  Serial.begin(115200);
  controller.configPins(A0, 2, 3);
  controller.configMapping(-20, 236, 20, 588, 407);
  controller.configPWM(255, 0, 5);
  controller.configLimits(236, 588);
}

void loop() {
  controller.main();
}

