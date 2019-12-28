#include <RGBmatrixPanel.h>
#include <hardwareSerial.h>
#include <string.h>

#define CLK  8
#define OE   9
#define LAT 10
#define A   A0
#define B   A1
#define C   A2

#define CMDBUFFER_SIZE 33

RGBmatrixPanel matrix(A, B, C, CLK, LAT, OE, false);

char text[33] = "Saved: 0 coffee cups";

int16_t    textX         = matrix.width(),
           textMin       = sizeof(text) * -12;

void setup() {
  matrix.begin();
  matrix.setTextWrap(false); // Allow text to run off right edge
  matrix.setTextSize(2);
  
  Serial.begin(9600);
  Serial.println("<Arduino is ready>");
}

void loop() {
  // Clear background
  matrix.fillScreen(0);

  matrix.setTextColor(matrix.Color333(0, 0, 7));
  matrix.setCursor(textX, 1);
  matrix.print(text);

  // Move text left (w/wrap), increase hue
  if((--textX) < textMin) textX = matrix.width();
  
  delay(20);
  // Update display
  matrix.swapBuffers(false);
}

void serialEvent()
{
 static char cmdBuffer[CMDBUFFER_SIZE] = "";
 char c;
 while(Serial.available())
 {
   c = processCharInput(cmdBuffer, Serial.read());
   Serial.print(c);
   if (c == '\n')
   {        
     strncpy(text, cmdBuffer, 18);
     textMin = sizeof(text) * -12;
     
     cmdBuffer[0] = 0;
     
     matrix.swapBuffers(false);
   }
 }
 delay(1);
}

char processCharInput(char* cmdBuffer, const char c)
{
 //Store the character in the input buffer
 if (c >= 32 && c <= 126) //Ignore control characters and special ascii characters
 {
   if (strlen(cmdBuffer) < CMDBUFFER_SIZE)
   {
     strncat(cmdBuffer, &c, 1);   //Add it to the buffer
   }
   else  
   {  
     return '\n';
   }
 }

 return c;
}
