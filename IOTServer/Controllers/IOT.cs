using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

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
            var t = ZPF.IOT.IOTHelper.GetTemp();

            if (string.IsNullOrEmpty(ZPF.IOT.IOTHelper.LastMessage))
            {
                return new TStats
                {
                    Temp = t,
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
