﻿using System;
using System.Drawing;
using System.IO;

namespace Hangfire.Web.BackgroundJobs
{
    public class DelayedJobs
    {

        public static string AddWatermarkJob(string filename, string watermarkText)
        {
            return Hangfire.BackgroundJob.Schedule(() => ApplyWatermark(filename, watermarkText), TimeSpan.FromSeconds(20));
        }

        public static void ApplyWatermark(string filename, string watermarkText)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", filename);

            using (var bitmap = Bitmap.FromFile(path))
            {
                using (Bitmap tempBitmap = new Bitmap(bitmap.Width, bitmap.Height))
                {
                    using (Graphics grp = Graphics.FromImage(tempBitmap))
                    {
                        grp.DrawImage(bitmap, 0, 0);

                        var font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold);

                        var color = Color.FromArgb(255, 0, 0);

                        var brush = new SolidBrush(color);

                        var point = new Point(20, bitmap.Height - 50);

                        grp.DrawString(watermarkText, font, brush, point);

                        tempBitmap.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures/watermarks", filename));
                    }

                }
            }
        }
    }
}
