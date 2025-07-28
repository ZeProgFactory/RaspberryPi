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

      private async void OnCounterClicked(object? sender, EventArgs e)
      {
         string url = $"{Server}/device/all_status?show_info=true&no_shared=true&auth_key={AuthorizationCloudKey}";

         using HttpClient client = new HttpClient();
         HttpResponseMessage response = await client.GetAsync(url);
         string json = await response.Content.ReadAsStringAsync();

         var allDevices = JsonSerializer.Deserialize<AllDevices>(json);

         var temperature = allDevices.data.devices_status._5432045b561c.temperature0.tC;
         var humidity = allDevices.data.devices_status._5432045b561c.humidity0.rh;
         var battery = allDevices.data.devices_status._5432045b561c.devicepower0.battery.percent;

         var temperature = allDevices.data.devices_status. data. devices_status. _5432045b561c.temperature0.tC;
         var humidity = allDevices.data.devices_status._5432045b561c.humidity0.rh;
         var battery = allDevices.data.devices_status._5432045b561c.devicepower0.battery.percent;


         CounterBtn.Text = $"Temperature: {temperature} °C - humidity {humidity} %";
      }
   }
}
