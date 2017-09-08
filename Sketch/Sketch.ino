int incomingByte = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial.println("Initialized \n");
}

void loop() {

  while (Serial.available() > 0)
    {
        if (Serial.available() > 0) {
                // read the incoming byte:
                incomingByte = Serial.read();

                // Only for debug. say what you got:
                //Serial.print("I received: ");
                //Serial.println(incomingByte);
                PORTD = incomingByte;
         }
    }
}
