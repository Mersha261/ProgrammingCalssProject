using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace ProgramingCalssProject.Models.Utillity
{
    public class ImageProccess
    {
        public ErrorPic CheckPic(IFormFile Pic, string Text)
        {
            ErrorPic e = new ErrorPic();

            if (Pic.Length == 0)
            {
                e.Text = Text + "در بارگزاری تصویر مشکلی پیش آمده لطفا مجددا اقدام نمایید";
                e.Error = true;
                return e;
            }

            //چک میکنیم حجم تصویر از ده مگابایت بیشتر نباید
            
            if (Pic.Length > 10000 * 1024)
            {
                e.Text = Text + " انتخابی حجمی بالاتر از 10 مگابایت دارد، لطفا تصویری با حجم کمتر انتخاب نمایید";
                e.Error = true;
                return e;
            }

            string strfilename = Path.GetFileName(Pic.FileName);
            string strfileextension = Path.GetExtension(strfilename).ToLower();
            if ((strfileextension != ".jpg") &&
                (strfileextension != ".jpeg") &&
                (strfileextension != ".jpe") &&
                (strfileextension != ".png") &&
                (strfileextension != ".bmp") &&
                (strfileextension != ".ico") &&
                (strfileextension != ".icon"))
            {
                e.Text = Text + "انتخابی از نوع تصویر نیست، لطفا تصویر معتبر دیگری انتخاب نمایید";
                return e;
            }

            string strcontenttype = Pic.ContentType.ToLower();
            if ((strcontenttype != "image/jpeg") &&
                (strcontenttype != "image/pjpeg"))
            {
                e.Text = Text + " انتخابی از نوع تصویر نیست، لطفا تصویر معتبر دیگری انتخاب نمایید";
                e.Error = true;
                return e;
            }


            e.Error = false;
            e.Text = "OK";
            return e;
        }
        public Image ResizeImage(Bitmap image)
        {
            //قابلیت تغییر این دو مورد زیر بر اساس نیاز مشتری است 
            float maxWidth = 900;
            float maxHeight = 900;

            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = maxWidth / originalWidth;
            float ratioY = maxHeight / originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, 72);
            encoderParameters.Param[0] = encoderParameter;
            return (Image)newImage;

        }
        public Image GetThumb(Image Pic)
        {
            Image image = Pic;
            float imgWidth = image.PhysicalDimension.Width;
            float imgHeight = image.PhysicalDimension.Height;
            float imgSize = imgHeight > imgWidth ? imgHeight : imgWidth;
            float imgResize = imgSize <= 300 ? (float)1.0 : 300 / imgSize;
            imgWidth *= imgResize; imgHeight *= imgResize;
            Image thumb = image.GetThumbnailImage((int)imgWidth, (int)imgHeight, delegate () { return false; }, (IntPtr)0);
            string ThumbName = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + "Thumb\\";
            return thumb;
        }

        public class ErrorPic
        {
            public Boolean Error { get; set; }
            public string Text { get; set; }
        }

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        public class AdStatus
        {
            public bool Error { get; set; }
            public string Text { get; set; }

        }
    }
}
