int incomingByte = 0;

int bitstat1;
int bitstat2;
int bitstat3;
int bitstat4;
int bitstat5;
int bitstat6;
int bitstat7;
int bitstat8;

void setup() {

  DDRD = DDRD | B11111110;
  DDRB = B00000011;
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
                bitstat1 = incomingByte & 0x01;
                bitstat2 = incomingByte & 0x02;
                bitstat3 = incomingByte & 0x04;
                bitstat4 = incomingByte & 0x08;
                bitstat5 = incomingByte & 0x10;
                bitstat6 = incomingByte & 0x20;
                bitstat7 = incomingByte & 0x40;
                bitstat8 = incomingByte & 0x80;

                
                // Only for debug. say what you got:
                //Serial.print("I received: ");
                //Serial.println(incomingByte);
                //PORTC = PORTC & 0b11110111; // clear bit 3, leave rest alone. 1 AND x = x, 0 AND x = 0
                //PORTC = PORT | 0b00001000; // set bit 3, leave rest alone/  0 OR x = x, 1 OR x = 1

                if (bitstat1 == 0x01)
                {
                  PORTD |= B00000100;
                }
                else if (bitstat1 == 0)
                {
                  //PORTD &= ~_BV(BUSP1);  //Deprecated
                  PORTD &= B11111011;
                }

                if (bitstat2 == 0x02)
                {
                  PORTD |= B00001000;
                }
                else if (bitstat2 == 0)
                {
                  PORTD &= B11110111;
                }

                if (bitstat3 == 0x04)
                {
                  PORTD |= B00010000;
                }
                else if (bitstat3 == 0)
                {
                  PORTD &= B11101111;
                }

                if (bitstat4 == 0x08)
                {
                  PORTD |= B00100000;
                }
                else if (bitstat4 == 0)
                {
                  PORTD &= B11011111;
                }

                if (bitstat5 == 0x10)
                {
                  PORTD |= B01000000;
                }
                else if (bitstat5 == 0)
                {
                  PORTD &= B10111111;
                }

                if (bitstat6 == 0x20)
                {
                  PORTD |= B10000000;
                }
                else if (bitstat6 == 0)
                {
                  PORTD &= B01111111;
                }

                if (bitstat7 == 0x40)
                {
                  PORTB |= B00000001;
                }
                else if (bitstat7 == 0)
                {
                  PORTB &= B11111110;
                }

                if (bitstat8 == 0x80)
                {
                  PORTB |= B00000010;
                }
                else if (bitstat8 == 0)
                {
                  PORTB &= B11111101;
                }
         }
    }
}
