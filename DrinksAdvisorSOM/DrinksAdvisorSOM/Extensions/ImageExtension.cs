using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DrinksAdvisorSOM.Extensions
{
    public static class ImageExtension
    {
        public static Image ResizeImage(this Image image, Size newSize, bool preserveAspectRatio = true)
        {
            int newWidth, newHeight;

            if (preserveAspectRatio)
            {
                int originalWidth = image.Width,
                    originalHeight = image.Height;

                double widthRatio = newSize.Width / (double)originalWidth,
                       heightRatio = newSize.Height / (double)originalHeight;

                double ratio = Math.Min(widthRatio, heightRatio);
                newWidth = (int)(originalWidth * ratio);
                newHeight = (int)(originalHeight * ratio);
            }
            else
            {
                newWidth = newSize.Width;
                newHeight = newSize.Height;
            }

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));
            }
            return newImage;
        }
    }
}
