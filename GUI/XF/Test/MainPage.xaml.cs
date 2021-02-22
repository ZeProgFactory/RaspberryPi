using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test
{
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();

         label.Text = "timer running...";
         Device.StartTimer(new TimeSpan(0, 0, 1), () =>
         {
            try
            {
               // do something every 1 seconds
               Device.BeginInvokeOnMainThread(() =>
               {
                  // interact with UI elements
                  label.Text = DateTime.Now.ToString("HH:mm:ss");
               });
            }
            catch { };

            return true; // runs again, or false to stop
         });
      }

      private void Button_Clicked(object sender, EventArgs e)
      {
         Environment.Exit(0);
      }
   }
}
