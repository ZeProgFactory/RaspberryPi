using Microsoft.AspNetCore.Mvc;
using ZPF;
using ZPF.SQL;

namespace IOTServer.Controllers
{
    public class ToolsController : Controller
   {
      [AllowCrossSiteJson]
      [Route("~/Tools/now")]
      [HttpGet]
      public string GetNow()
      {
         string lang = "";

         return GetNow(lang);
      }

      [AllowCrossSiteJson]
      [Route("~/Tools/now/{lang}")]
      [HttpGet]
      public string GetNow([FromRoute] string lang = "")
      {
         var userLangs = Request.Headers["Accept-Language"].ToString();

         var firstLang = userLangs.Split(',').FirstOrDefault();
         firstLang = firstLang.Split('-').FirstOrDefault().ToLower();

         var defaultLang = string.IsNullOrEmpty(firstLang) ? "en" : firstLang;
         if (("*en*de*fr*").IndexOf(lang.ToLower()) > 0) defaultLang = lang.ToLower();

         switch (defaultLang)
         {
            case "de": return DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"); break;
            case "fr": return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); break;

            default:
            case "en": return DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"); break;
         };
      }

      [AllowCrossSiteJson]
      [Route("~/Tools/TestDB")]
      [HttpGet]
      public string GetTestDB()
      {
         string Result = "";

         MainViewModel.Current.Prologue(Request);

         try
         {
            if (!string.IsNullOrEmpty(MainViewModel.Current.Connection.LastError))
            {
               return MainViewModel.Current.Connection.LastError;
            };
         }
         catch (Exception ex)
         {
            return "(1) " + ex.Message;
         };

         if (MainViewModel.Current.Connection.DbConnection.State != System.Data.ConnectionState.Open)
         {
            MainViewModel.Current.Connection.CheckConnection();
         };

         Result = DB_SQL.QuickQuery("SELECT sqlite_version();");

         if (!string.IsNullOrEmpty(MainViewModel.Current.Connection.LastError))
         {
            return MainViewModel.Current.Connection.LastError;
         };

         return Result;
      }

   }
}
