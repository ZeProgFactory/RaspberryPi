using SkiaSharp;
using SkiaSharp.Views.UWP;
using System;
using System.Timers;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SkiaClock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Create a timer with a two second interval.
            var aTimer = new System.Timers.Timer(500);

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += ATimer_Elapsed;  
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            canvasView.Invalidate();
        }

        float revolveDegrees, rotateDegrees;

        void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            using (SKPaint strokePaint = new SKPaint())
            using (SKPaint fillPaint = new SKPaint())
            {
                strokePaint.Style = SKPaintStyle.Stroke;
                strokePaint.Color = SKColors.Black;
                strokePaint.StrokeCap = SKStrokeCap.Round;

                fillPaint.Style = SKPaintStyle.Fill;
                fillPaint.Color = SKColors.Gray;

                // Transform for 100-radius circle centered at origin
                canvas.Translate(info.Width / 2f, info.Height / 2f);
                canvas.Scale(Math.Min(info.Width / 200f, info.Height / 200f));

                // Hour and minute marks
                for (int angle = 0; angle < 360; angle += 6)
                {
                    canvas.DrawCircle(0, -90, angle % 30 == 0 ? 4 : 2, fillPaint);
                    canvas.RotateDegrees(6);
                }

                DateTime dateTime = DateTime.Now;

                // Hour hand
                strokePaint.StrokeWidth = 10;
                canvas.Save();
                canvas.RotateDegrees(30 * dateTime.Hour + dateTime.Minute / 2f);
                canvas.DrawLine(0, 0, 0, -50, strokePaint);
                canvas.Restore();

                // Minute hand
                strokePaint.StrokeWidth = 10;
                canvas.Save();
                canvas.RotateDegrees(6 * dateTime.Minute + dateTime.Second / 10f);
                canvas.DrawLine(0, 0, 0, -70, strokePaint);
                canvas.Restore();

                // Second hand
                strokePaint.StrokeWidth = 2;
                canvas.Save();
                canvas.RotateDegrees(6 * dateTime.Second);
                canvas.DrawLine(0, 10, 0, -80, strokePaint);
                canvas.Restore();
            }
        }
    }
}
