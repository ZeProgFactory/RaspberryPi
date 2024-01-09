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
    }
}
