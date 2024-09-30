// definimos el pin analogico donde esta conectado el sensor 
#define LM35PIN A0 

void setup() {
  Serial.begin(9600);
}

void loop() {
  // almacenamos el valor analogico del sensor
  int valor = analogRead(LM35PIN);

  float voltaje = valor * (5.0 / 1023.0); // convertimos el valor a voltaje
  float tempC = voltaje * 100; // convertimos el voltaje en temperatura calcius

  Serial.print("Temperatura: ");
  Serial.println(tempC, 2); // Envía la temperatura con 2 decimales

  // espera para la siguiente lectura
  delay(2000);
}