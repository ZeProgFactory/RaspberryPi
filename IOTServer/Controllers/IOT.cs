using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace IOTServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IOT : ControllerBase
    {
        [HttpGet(Name = "GetTemp")]
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
