using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;

class Program
{
    private const string MqttBrokerAddress = "test.mosquitto.org";
    private const string MqttTopic = "SLS";

    static async Task Main(string[] args)
    {
        Console.WriteLine("MQTT to HTTP Redirector started...");

        var mqttFactory = new MqttFactory();
        var mqttClient = mqttFactory.CreateMqttClient();

        var mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(MqttBrokerAddress, 1883)
            .Build();

        mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            var message = e.ApplicationMessage?.Payload == null
                ? string.Empty
                : Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            Console.WriteLine($"Received MQTT Message: {message}");

            if (!string.IsNullOrWhiteSpace(message))
            {
                await SendDataToApi(message);
            }
            else
            {
                Console.WriteLine("Received empty message.");
            }
        };

        try
        {
            await mqttClient.ConnectAsync(mqttOptions);
            Console.WriteLine("Connected to MQTT broker.");
            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(MqttTopic).Build());
            Console.WriteLine($"Subscribed to topic: {MqttTopic}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to MQTT broker: {ex.Message}");
            return;
        }


        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

        await mqttClient.DisconnectAsync();
    }

    private static async Task SendDataToApi(string jsonData)
    {
        Console.WriteLine($"Sending data to API: {jsonData}");

        using (var httpClient = new HttpClient())
        {
            try
            {
                var data = JsonConvert.DeserializeObject<dynamic>(jsonData);
                int id = data.id;
                string type = data.type;

                if (type == "SensorData")
                {
                    string endpoint = $"http://localhost:5149/api/Sensor/data/{id}";
                    int sensorData = data.data;
                    var content = new StringContent(sensorData.ToString(), Encoding.UTF8, "application/json");

                    var response = await httpClient.PutAsync(endpoint, content);

                    Console.WriteLine($"HTTP Status Code: {response.StatusCode}");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Data for Sensor ID {id} sent successfully!");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to send data for Sensor ID {id}. Status Code: {response.StatusCode}");
                    }
                }
                else if (type == "StreetlightBrightness")
                {
                    string endpoint = $"http://localhost:5149/api/StreetLight/brightness/{id}";
                    var response = await httpClient.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string brightness = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Streetlight brightness for ID {id}: {brightness}");
                        await SendBrightnessBackToDevice(id, brightness);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to get brightness for Streetlight ID {id}. Status Code: {response.StatusCode}");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending data to API: {ex.Message}");
            }
        }
    }

    private static async Task SendBrightnessBackToDevice(int id, string brightness)
    {
        var mqttFactory = new MqttFactory();
        var mqttClient = mqttFactory.CreateMqttClient();

        var mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(MqttBrokerAddress, 1883)
            .Build();

        try
        {
            await mqttClient.ConnectAsync(mqttOptions);
            Console.WriteLine("Connected to MQTT broker.");

            string topic = $"SLS_Brightness_{id}";

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(brightness)
                .Build();

            await mqttClient.PublishAsync(message);
            Console.WriteLine($"Sent brightness data: {brightness} to topic {topic} via MQTT.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending brightness data via MQTT: {ex.Message}");
        }
    }

}
