using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using ZPF;
using ZPF.SQL;

namespace IOTServer.Controllers
{
    [ApiController]
    public class IOT : ControllerBase
    {
        [AllowCrossSiteJson]
        [Route("~/IOT/GetTemp")]
        [HttpGet]
        public string GetTemp()
        {
            var t = ZPF.IOT.IOTHelper.GetTemp();

            if (string.IsNullOrEmpty(ZPF.IOT.IOTHelper.LastMessage))
            {
                return $"{t} °C";
            }
            else
            {
                return ZPF.IOT.IOTHelper.LastMessage;
            };
        }

        [AllowCrossSiteJson]
        [Route("~/IOT/GetStats")]
        [HttpGet]
        public TStats GetTStats()
        {
            MainViewModel.Current.OpenDB();

            var st = DB_SQL.QuickQuery("select Max( TimeStamp ) from Reading");
            var t = ZPF.IOT.IOTHelper.GetTemp();


            if (string.IsNullOrEmpty(st) || (DateTime.Now - DateTime.Parse(st)) > TimeSpan.FromHours(1))
            {
                DB_SQL.Insert(new Reading { Temp = (decimal)t });
            };


            var list = DB_SQL.Query<Reading>("select * from Reading");

            if (string.IsNullOrEmpty(ZPF.IOT.IOTHelper.LastMessage))
            {
                return new TStats
                {
                    Temp = t,
                    TempMax24 = (double)list.Where(x => x.TimeStamp > DateTime.Now.AddHours(-24)).Max(x => x.Temp),
                    TempMax48 = (double)list.Where(x => x.TimeStamp > DateTime.Now.AddHours(-48)).Max(x => x.Temp),
                    TempMin48 = (double)list.Where(x => x.TimeStamp > DateTime.Now.AddHours(-48)).Min(x => x.Temp),
                    TempMin24 = (double)list.Where(x => x.TimeStamp > DateTime.Now.AddHours(-24)).Min(x => x.Temp),
                };
            }
            else
            {
                return new TStats
                {
                    LastMessage = ZPF.IOT.IOTHelper.LastMessage,
                };
            };
        }
    }
}
