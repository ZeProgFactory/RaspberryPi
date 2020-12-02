using System.IO;

using SkiaSharp;

namespace SkiaSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // crate a surface
            var info = new SKImageInfo(1920, 1080);
            using (var surface = SKSurface.Create(info))
            {
                // the the canvas and properties
                var canvas = surface.Canvas;

                // make sure the canvas is blank
                canvas.Clear(SKColors.Black);

                // draw some text
                var paint = new SKPaint
                {
                    Color = SKColors.White,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = 64,

                };

                var coord = new SKPoint(info.Width / 2, (info.Height + paint.TextSize) / 2);

                string st = $"{System.Environment.MachineName} - {System.Environment.OSVersion} - {System.Environment.Version}";

                canvas.DrawText(st, coord, paint);

                // save the file
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite("Background.png"))
                {
                    data.SaveTo(stream);
                }
            }
        }
    }
}
