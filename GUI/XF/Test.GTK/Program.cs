using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace Test.GTK
{
   class MainClass
   {
      [STAThread]
      public static void Main(string[] args)
      {
         Gtk.Application.Init();
         Forms.Init();

         var app = new App();
         var window = new FormsWindow();

         window.Maximize();
         window.Fullscreen();

         window.LoadApplication(app);
         window.SetApplicationTitle("Xamarin.Forms on RasPi");
         window.Show();

         Gtk.Application.Run();
      }
   }
}
