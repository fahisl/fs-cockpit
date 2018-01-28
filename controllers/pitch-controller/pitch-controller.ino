// FS Platform Controller 

#include "Controller.h"

Controller controller("bcbc7cc9-f2be-4303-87f1-9ca2e0378e0b");

void setup() {
  Serial.begin(115200);
  controller.configPins(A0, 2, 3);
  controller.configMapping(-10, 391, 10, 543, 467);
  controller.configPWM(255, 0, 5);
  controller.configLimits(281, 543);
}

void loop() {
  controller.main();
}

