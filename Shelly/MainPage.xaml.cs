using System.Text.Json;
using Shelly.Models;

namespace Shelly
{
   public partial class MainPage : ContentPage
   {
      int count = 0;

      public MainPage()
      {
         InitializeComponent();
      }

      string Server = @"https://shelly-191-eu.shelly.cloud";
      string AuthorizationCloudKey = "MzQzMTAxdWlk1054AA7FAE4A26BE57959AFD4B040511A4BD22F3528BAADE8399655865E380B2FAFD596F34816CC4";
      string DeviceID = "5432045b561c";

      private async void OnCounterClicked(object? sender, EventArgs e)
      {
         string url = $"{Server}/device/all_status?show_info=true&no_shared=true&auth_key={AuthorizationCloudKey}";
         //string url = $"{Server}/device/all_status?show_info=true&no_shared=true&auth_key={AuthorizationCloudKey}&id={DeviceID}";

         using HttpClient client = new HttpClient();
         HttpResponseMessage response = await client.GetAsync(url);
         string json = await response.Content.ReadAsStringAsync();

         Temperature0 temperature;
         Devicepower0 devicepower;
         Humidity0 humidity;
         Wifi wifi;

         using (var jsonDoc = JsonDocument.Parse(json))
         {
            var data = jsonDoc.RootElement.GetProperty("data");
            var devices = data.GetProperty("devices_status");
            var HT01 = devices.GetProperty("5432045b561c");

            var jsonT = HT01.GetProperty("temperature:0");
            temperature = jsonT.Deserialize<Temperature0>();

            var jsonP = HT01.GetProperty("devicepower:0");
            devicepower = jsonP.Deserialize<Devicepower0>();

            var jsonH = HT01.GetProperty("humidity:0");
            humidity = jsonH.Deserialize<Humidity0>();

            var jsonW = HT01.GetProperty("wifi");
            wifi = jsonW.Deserialize<Wifi>();
         }

         //json = json.Replace("temperature:0", "temperature0");
         //json = json.Replace("devicepower:0", "devicepower0");
         //json = json.Replace("humidity:0", "humidity0");

         //var allDevices = JsonSerializer.Deserialize<AllDevices>(json, new JsonSerializerOptions {  });

         //var temperature = allDevices.data.devices_status._5432045b561c.temperature0.tC;
         //var humidity = allDevices.data.devices_status._5432045b561c.humidity0.rh;
         //var battery = allDevices.data.devices_status._5432045b561c.devicepower0.battery.percent;

         CounterBtn.Text = $"{temperature.tC} °C - {humidity.rh} % - ({devicepower.battery.percent} % / {wifi.sta_ip} {wifi.rssi} dB)";
      }
   }
}

/*


https://shelly-XX-eu.shelly.cloud/device/Status&auth_key=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx&id=DeviceID deiner Shelly

Energiedaten:

https://shelly-XX-eu.shelly.cloud/statistics/emeter/consumption?auth_key=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx&channel=X&id=DeviceID deiner Shelly


https://loxwiki.atlassian.net/wiki/spaces/LOX/pages/1596489862/Shelly+per+Cloud+abfragen+und+steuern

 {"isok":true,"data":{"devices_status":{"5432045b561c":{"ws":{"connected":false},"mqtt":{"connected":false},"ht_ui":[],"temperature:0":{"id":0,"tC":17.6,"tF":63.7},"ble":[],"devicepower:0":{"id":0,"battery":{"V":5.35,"percent":67},"external":{"present":false}},"sys":{"mac":"5432045B561C","restart_required":false,"time":"22:16","unixtime":1753733762,"uptime":2,"ram_size":256248,"ram_free":145480,"fs_size":1048576,"fs_free":700416,"cfg_rev":17,"kvs_rev":0,"webhook_rev":0,"available_updates":[],"wakeup_reason":{"boot":"deepsleep_wake","cause":"status_update"},"wakeup_period":7200,"reset_reason":8},"ts":1753733762.03,"serial":1753733762,"id":"5432045b561c","humidity:0":{"id":0,"rh":63.9},"_updated":"2025-07-28 20:16:02","code":"S3SN-0U12A","wifi":{"sta_ip":"192.168.10.91","status":"got ip","ssid":"ZPF-AP","rssi":-85},"cloud":{"connected":true},"_sleeping":true,"_dev_info":{"id":"5432045b561c","gen":"G2","code":"S3SN-0U12A","online":true}}},"pending_notifications":{}}}

 
 
 */
