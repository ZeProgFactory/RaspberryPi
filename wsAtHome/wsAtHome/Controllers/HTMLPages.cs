using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ZPF;

namespace wsAtHome.Controllers
{
   public class HTMLPagesController : ControllerBase
   {
      string HTMLTemplate = @"
<!DOCTYPE html>
<html>
<head>
<link href='/Static/my.css' rel='stylesheet'>
<title>HomeBox - {#Title#}</title>
</head>
<body>
<br/>
<h1>HomeBox - {#Title#}</h1>
{#Body#}
<br/>
Page generated {#Now#}<br/>
</body>
</html>
";

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -    

      // https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types

      public static readonly Dictionary<string, string> mimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
      {
         {".asf", "video/x-ms-asf"},
         {".asx", "video/x-ms-asf"},
         {".avi", "video/x-msvideo"},
         {".bin", "application/octet-stream"},
         {".cco", "application/x-cocoa"},
         {".crt", "application/x-x509-ca-cert"},
         {".css", "text/css"},
         {".deb", "application/octet-stream"},
         {".der", "application/x-x509-ca-cert"},
         {".dll", "application/octet-stream"},
         {".dmg", "application/octet-stream"},
         {".ear", "application/java-archive"},
         {".eot", "application/octet-stream"},
         {".exe", "application/octet-stream"},
         {".flv", "video/x-flv"},
         {".gif", "image/gif"},
         {".hqx", "application/mac-binhex40"},
         {".htc", "text/x-component"},
         {".htm", "text/html"},
         {".html", "text/html"},
         {".ico", "image/x-icon"},
         {".img", "application/octet-stream"},
         {".iso", "application/octet-stream"},
         {".jar", "application/java-archive"},
         {".jardiff", "application/x-java-archive-diff"},
         {".jng", "image/x-jng"},
         {".jnlp", "application/x-java-jnlp-file"},
         {".jpeg", "image/jpeg"},
         {".jpg", "image/jpeg"},
         {".js", "application/x-javascript"},
         {".mml", "text/mathml"},
         {".mng", "video/x-mng"},
         {".mov", "video/quicktime"},
         {".mp3", "audio/mpeg"},
         {".mpeg", "video/mpeg"},
         {".mpg", "video/mpeg"},
         {".msi", "application/octet-stream"},
         {".msm", "application/octet-stream"},
         {".msp", "application/octet-stream"},
         {".pdb", "application/x-pilot"},
         {".pdf", "application/pdf"},
         {".pem", "application/x-x509-ca-cert"},
         {".pl", "application/x-perl"},
         {".pm", "application/x-perl"},
         {".png", "image/png"},
         {".prc", "application/x-pilot"},
         {".ra", "audio/x-realaudio"},
         {".rar", "application/x-rar-compressed"},
         {".rpm", "application/x-redhat-package-manager"},
         {".rss", "text/xml"},
         {".run", "application/x-makeself"},
         {".sea", "application/x-sea"},
         {".shtml", "text/html"},
         {".sit", "application/x-stuffit"},
         {".svg", "image/svg+xml"},
         {".swf", "application/x-shockwave-flash"},
         {".tcl", "application/x-tcl"},
         {".tk", "application/x-tcl"},
         {".txt", "text/plain"},
         {".war", "application/java-archive"},
         {".wbmp", "image/vnd.wap.wbmp"},
         {".wmv", "video/x-ms-wmv"},
         {".xml", "text/xml"},
         {".xpi", "application/x-xpinstall"},
         {".zip", "application/zip"},
         {".map", "application/json"}
      };

      public string GetMimeType(string Extension)
      {
         try
         {

            return mimeTypes[Extension];
         }
         catch
         {
            if (string.IsNullOrEmpty(Extension))
            {
               return mimeTypes[".txt"];
            }
            else
            {
               return mimeTypes[".bin"];
            };
         };
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  

      [HttpGet]
      [Route("~/")]
      public ActionResult GetHomePage()
      {
         return GetFileNamePage("index.html");
      }

      [HttpGet]
      [Route("~/{FileName}")]
      public ActionResult GetFileNamePage([FromRoute] string FileName)
      {
         return GetFolderPage("", FileName);
      }

      [HttpGet]
      [Route("~/{Folder}/{FileName}")]
      public ActionResult GetFolderPage([FromRoute] string Folder, [FromRoute] string FileName)
      {
         return GetFolder2Page("", Folder, FileName);
      }

      [HttpGet]
      [Route("~/{Folder1}/{Folder2}/{FileName}")]
      public ActionResult GetFolder2Page([FromRoute] string Folder1, [FromRoute] string Folder2, [FromRoute] string FileName)
      {
         return GetFolder3Page("", Folder1, Folder2, FileName);
      }

      [HttpGet]
      [Route("~/{Folder1}/{Folder2}/{Folder3}/{FileName}")]
      public ActionResult GetFolder3Page([FromRoute] string Folder1, [FromRoute] string Folder2, [FromRoute] string Folder3, [FromRoute] string FileName)
      {
         return GetFolder4Page("", Folder1, Folder2, Folder3, FileName);
      }

      [HttpGet]
      [Route("~/{Folder1}/{Folder2}/{Folder3}/{Folder4}/{FileName}")]
      public ActionResult GetFolder4Page([FromRoute] string Folder1, [FromRoute] string Folder2, [FromRoute] string Folder3, [FromRoute] string Folder4, [FromRoute] string FileName)
      {
         return GetFolder5Page("", Folder1, Folder2, Folder3, Folder4, FileName);
      }

      [HttpGet]
      [Route("~/{Folder1}/{Folder2}/{Folder3}/{Folder4}/{Folder5}/{FileName}")]
      public ActionResult GetFolder5Page([FromRoute] string Folder1, [FromRoute] string Folder2, [FromRoute] string Folder3, [FromRoute] string Folder4, [FromRoute] string Folder5, [FromRoute] string FileName)
      {

         string contentType = GetMimeType(System.IO.Path.GetExtension(FileName));

         if (string.IsNullOrEmpty(contentType))
         {
         };

         // - - -   - - -  

         if (Folder1 != "" || Folder2 != "" || Folder3 != "" || Folder4 != "" || Folder5 != "")
         {
            FileName = Folder1 + @"\" + Folder2 + @"\" + Folder2 + @"\" + Folder4 + @"\" + Folder5 + @"\" + FileName;
         };

         FileName = Environment.CurrentDirectory + @"\Root/" + FileName;
         FileName = MainViewModel.Current.CleanPath(FileName);

         if (!System.IO.File.Exists(FileName))
         {
            Response.StatusCode = 404;
            return NotFound();
         };

         // - - -   - - -  

         return new FileStreamResult(new FileStream(FileName, FileMode.Open), contentType);
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      [HttpGet]
      [Route("~/Info")]
      public ContentResult GetInfoPage()
      {
         var body = "";
         body += @"<a href='/'>Home</a><br/>";
         body += @"<a href='/List'>list serial ports</a><br/>";
         body += @"<a href='/Init'>init beeper</a><br/>";
         body += @"<a href='/Call'>call beeper</a><br/>";
         body += @"<br/>";

         // - - -   - - -  

         TStrings info = new TStrings();

         info.Add($"IP=[{GetLocalIPAddress()}]");
         info.Add($"MachineName=[{Environment.MachineName}]");
         info.Add($"OSVersion=[{Environment.OSVersion}]");
         info.Add($"CurrentDirectory=[{Environment.CurrentDirectory}]");
         info.Add($"DataFolder=[{MainViewModel.Current.DataFolder}]");

         body += info.HTMLTable();
         body += @"<br/>";

         // - - -   - - -  

         return HTMLSource(HTMLTemplate, "Infos", body);
      }

      public static string GetLocalIPAddress()
      {
         var host = Dns.GetHostEntry(Dns.GetHostName());
         foreach (var ip in host.AddressList)
         {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
               return ip.ToString();
            }
         }
         throw new Exception("No network adapters with an IPv4 address in the system!");
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

      public ContentResult HTMLSource(string text, string title, string body)
      {
         text = text.Replace("{#Now#}", DateTime.Now.ToString());
         text = text.Replace("{#Title#}", title);
         text = text.Replace("{#Body#}", body);

         return new ContentResult
         {
            ContentType = "text/html",
            StatusCode = (int)HttpStatusCode.OK,
            Content = text
         };
      }

      // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -
   }
}
