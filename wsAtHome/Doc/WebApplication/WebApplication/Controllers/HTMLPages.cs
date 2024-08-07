﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class HTMLPagesController : ControllerBase
    {
        string HTML = @"
<!DOCTYPE html>
<html>
<head>
<title>Page Title</title>
</head>
<body>
<h1>This is a Heading</h1>
<p>This is a paragraph.</p>
<a href='https://www.ZPF.fr'>site ZPF.fr</a>
{#Now#}
</body>
</html>
";

        [HttpGet]
        [Route("~/")]
        public ContentResult GetHomePage()
        {
            return HTMLSource(HTML);
        }

        // - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  - -  -

        public ContentResult HTMLSource(string text)
        {
            text = text.Replace("{#Now#}", DateTime.Now.ToString());

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
