#include <WiFi.h>
#include <PubSubClient.h>

#define WIFI_SSID "Wokwi-GUEST"
#define WIFI_PASSWORD ""
#define WIFI_CHANNEL 6

const char* mqtt_server = "test.mosquitto.org";
const char* mqtt_topic = "SLS";

WiFiClient espClient;
PubSubClient mqttClient(espClient);

const int LDR_PIN = 34;  
const int PIR_PIN = 23;  

const float GAMMA = 0.7;
const float RL10 = 50;
const float V_REF = 3.3;  

const int LIGHT_SENSOR_ID = 2;
const int MOTION_SENSOR_ID = 1;
const int STREET_LIGHT_ID = 1;

float lux = 0.0;
bool motionDetected = false;
String streetlightBrightness = "Unknown";

void setup_wifi() {
  WiFi.begin(WIFI_SSID, WIFI_PASSWORD, WIFI_CHANNEL);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi Connected!");
  Serial.println(WiFi.localIP());
}

void reconnect() {
  while (!mqttClient.connected()) {
    Serial.println("Attempting MQTT connection...");
    if (mqttClient.connect(("ESP32_" + String(STREET_LIGHT_ID)).c_str())) {
      Serial.println("MQTT Connected");
      
      bool subscribed = mqttClient.subscribe(("SLS_Brightness_" + String(STREET_LIGHT_ID)).c_str());
      if (subscribed) {
        Serial.println("Successfully subscribed to topic: SLS_Brightness_" + String(STREET_LIGHT_ID));
      } else {
        Serial.println("Failed to subscribe to topic: SLS_Brightness_" + String(STREET_LIGHT_ID));
      }
    }
    else {
      Serial.print("Failed to connect, rc=");
      Serial.print(mqttClient.state());
      Serial.println(" try again in 5 seconds");
      delay(5000);
    }
  }
}

void callback(char* topic, byte* payload, unsigned int length) {
  String message = "";
  for (unsigned int i = 0; i < length; i++) {
    message += (char)payload[i];
  }
  Serial.print("Message arrived on topic: ");
  Serial.println(topic);
  Serial.print("Message: ");
  Serial.println(message);

  if (String(topic) == "SLS_Brightness_" + String(STREET_LIGHT_ID)) {
    streetlightBrightness = message;
    Serial.print("Updated Brightness: ");
    Serial.println(streetlightBrightness);
  }
}

void sendLightData(float lux) {
  if (mqttClient.connected()) {
    String payload = String("{\"id\":") + LIGHT_SENSOR_ID + ",\"data\":" + lux + ",\"type\":\"SensorData\"}";
    Serial.println("Sending payload: " + payload);
    mqttClient.publish(mqtt_topic, payload.c_str());
  } else {
    Serial.println("MQTT not connected");
  }
}

void sendMotionData(bool motion) {
  if (mqttClient.connected()) {
    String payload = String("{\"id\":") + MOTION_SENSOR_ID + ",\"data\":" + (motion ? "1" : "0") + ",\"type\":\"SensorData\"}";
    Serial.println("Sending payload: " + payload);
    mqttClient.publish(mqtt_topic, payload.c_str());
  } else {
    Serial.println("MQTT not connected");
  }
}

void sendStreetlightBrightnessRequest() {
  if (mqttClient.connected()) {
    String payload = String("{\"id\":") + STREET_LIGHT_ID + ",\"type\":\"StreetlightBrightness\"}";
    Serial.println("Sending brightness request: " + payload);
    mqttClient.publish(mqtt_topic, payload.c_str());
  } else {
    Serial.println("MQTT not connected");
  }
}

void setup() {
  Serial.begin(115200);

  pinMode(LDR_PIN, INPUT);
  pinMode(PIR_PIN, INPUT);

  setup_wifi();
  mqttClient.setServer(mqtt_server, 1883);
  mqttClient.setCallback(callback);
}

void loop() {
  if (!mqttClient.connected()) {
    reconnect();
  }
  mqttClient.loop();

  int analogValue = analogRead(LDR_PIN);
  float voltage = analogValue / 4095.0 * V_REF;
  float resistance = 2000 * voltage / (V_REF - voltage);
  lux = pow((RL10 * 1e3 * pow(10, GAMMA)) / resistance, (1.0 / GAMMA)) / 10.0;

  motionDetected = digitalRead(PIR_PIN);

  Serial.print("LUX Value: ");
  Serial.println(lux);
  Serial.print("Motion Detected: ");
  Serial.println(motionDetected ? "Yes" : "No");
  Serial.print("Current Brightness: ");
  Serial.println(streetlightBrightness);

  sendLightData(lux);  
  sendMotionData(motionDetected);  

  sendStreetlightBrightnessRequest();  

  delay(5000);
}
