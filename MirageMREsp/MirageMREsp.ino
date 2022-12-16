const int servofreq = 50;
const int motorfreq = 200;
const int ledChannellf = 0;
const int ledChannellb = 1;
const int ledChannelrf = 2;
const int ledChannelrb = 3;
const int servoChannelL = 4;
const int servoChannelR = 5;
const int resolution = 8;

int lastservovalue = NULL;
int servoangle = NULL;
int power = 4;
int count = 20;
float delaytime = 50;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
  Serial.setDebugOutput(false);

  pinMode(13,OUTPUT);
  pinMode(12,OUTPUT);
  pinMode(26,OUTPUT);
  pinMode(27,OUTPUT);

  ledcSetup(ledChannellf, motorfreq, resolution);
  ledcSetup(ledChannellb, motorfreq, resolution);
  ledcSetup(ledChannelrf, motorfreq, resolution);
  ledcSetup(ledChannelrb, motorfreq, resolution);
  ledcSetup(servoChannelL, servofreq, resolution);
  ledcSetup(servoChannelR, servofreq, resolution);
  
  // attach the channel to the GPIO to be controlled
  ledcAttachPin(13, ledChannellf);
  ledcAttachPin(12, ledChannellb);
  ledcAttachPin(26, ledChannelrf);
  ledcAttachPin(27, ledChannelrb);
  ledcAttachPin(32, servoChannelL);
  ledcAttachPin(33, servoChannelR);
}

void loop() {
  // put your main code here, to run repeatedly:
  if(Serial){
    String x;
    while(Serial.available()>0){
      x+=(char)Serial.read();    
    }  
    ////Serial.print(x);
    char m = x.charAt(0);
    x.remove(0,1);
    int a = x.toInt();
    if(m=='0'){
      //Serial.print(x);
      ledcWrite(ledChannellf,0);
      ledcWrite(ledChannelrf,0);
      ledcWrite(ledChannellb,0);
      ledcWrite(ledChannelrb,0);    
    }
    if(m=='1'){  
      //Serial.println("front"); 
      pwmstep(a,a/power,count,delaytime,ledChannellf,ledChannelrf);
    }
    if(m=='2'){
      //Serial.println("back");
      pwmstep(a,a/power,count,delaytime,ledChannellb,ledChannelrb);
    }
    if(m=='3'){     
      //Serial.println("left");
      /*
      ledcWrite(ledChannellb,a);
      ledcWrite(ledChannelrf,a);
      delay(250);
      ledcWrite(ledChannellb,a/sub_power);
      ledcWrite(ledChannelrf,a/sub_power);
      delay(250);
      ledcWrite(ledChannellb,a/power);
      ledcWrite(ledChannelrf,a/power);
      delay(250);
      ledcWrite(ledChannellb,0);
      ledcWrite(ledChannelrf,a/power);
      */
      pwmstep(a, a/power,count,delaytime,ledChannellb,ledChannelrf);
    }
    if(m=='4'){    
      //Serial.println("right");
      /*
      ledcWrite(ledChannellf,a);
      ledcWrite(ledChannelrb,a);
      delay(250);
      ledcWrite(ledChannellf,a/sub_power);
      ledcWrite(ledChannelrb,a/sub_power);
      delay(250);
      ledcWrite(ledChannellf,a/power);
      ledcWrite(ledChannelrb,a/power);
      delay(250);
      ledcWrite(ledChannellf,a/power);
      ledcWrite(ledChannelrb,0);
      */
      pwmstep(a,a/power,count,delaytime,ledChannellf,ledChannelrb);
    }
    if(m=='5'){
      ledcWrite(ledChannellf,0);
      ledcWrite(ledChannelrf,0);
      ledcWrite(ledChannellb,0);
      ledcWrite(ledChannelrb,0); 
      ledcWrite(ledChannellf,a/power);
    }
    if(m=='6'){
      ledcWrite(ledChannellf,0);
      ledcWrite(ledChannelrf,0);
      ledcWrite(ledChannellb,0);
      ledcWrite(ledChannelrb,0); 
      ledcWrite(ledChannelrf,a/power);
    }
    if(m=='7'){
      ledcWrite(ledChannellf,0);
      ledcWrite(ledChannelrf,0);
      ledcWrite(ledChannellb,0);
      ledcWrite(ledChannelrb,0); 
      ledcWrite(ledChannelrb,a/power);
    }if(m=='8'){
      ledcWrite(ledChannellf,0);
      ledcWrite(ledChannelrf,0);
      ledcWrite(ledChannellb,0);
      ledcWrite(ledChannelrb,0); 
      ledcWrite(ledChannellb,a/power);
    }

    if(m=='u'){
      servo(45,servoChannelL,servoChannelR);
    }
    if(m=='d'){
      servo(5,servoChannelL,servoChannelR);
    }
    x = "";
    delay(10);
  }

}  

void pwmstep(int a, int power , int stp , int delaytime ,int channelA, int channelB){
  ledcWrite(ledChannellf,0);
  ledcWrite(ledChannelrf,0);
  ledcWrite(ledChannellb,0);
  ledcWrite(ledChannelrb,0);   
  for(a = 0 ; a <= power; a = a + stp){
    //Serial.println(a);
    ledcWrite(channelA,a);
    ledcWrite(channelB,a);
    delay(delaytime);
  }
  //Serial.print(power);
  ledcWrite(channelB,power);
  ledcWrite(channelB,power);
}

void servo(int angel,int channelA,int channelB){
  int servovalue = map(angel,0,180,6.4,30.72);
  ledcWrite(channelA,180-servovalue);
  ledcWrite(channelB,servovalue);
}
