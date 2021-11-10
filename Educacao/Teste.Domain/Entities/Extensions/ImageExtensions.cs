using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Teste.Domain.Entities.Extensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Retorna esta <see cref="Image"/> como texto base64.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ToBase64(this Image img)
        {
            return Convert.ToBase64String(img.ToBytes());
        }

        /// <summary>
        /// Retorna este <see cref="Bitmap"/> como texto base64.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ToBase64(this Bitmap img)
        {
            return Convert.ToBase64String(img.ToBytes());
        }

        /// <summary>
        /// Retorna esta <see cref="Image"/> como matriz de bytes.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Retorna este <see cref="Bitmap"/> como matriz de bytes.
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }

        /// <summary>
        /// Redimensionar este <see cref="Image"/>.
        /// </summary>
        /// <param name="SourceImage"> este <see cref="Image"/>.</param>
        /// <param name="NewHeight">Altura desejada.</param>
        /// <param name="NewWidth">Largura desejada.</param>
        /// <returns></returns>
        public static Image ImageResize(this Image SourceImage, Int32 NewHeight, Int32 NewWidth)
        {
            Bitmap bitmap = new Bitmap(NewWidth, NewHeight, SourceImage.PixelFormat);
            if (bitmap.PixelFormat == PixelFormat.Format1bppIndexed | bitmap.PixelFormat == PixelFormat.Format4bppIndexed | bitmap.PixelFormat == PixelFormat.Format8bppIndexed | bitmap.PixelFormat == PixelFormat.Undefined | bitmap.PixelFormat == PixelFormat.DontCare | bitmap.PixelFormat == PixelFormat.Format16bppArgb1555 | bitmap.PixelFormat == PixelFormat.Format16bppGrayScale)
                throw new NotSupportedException("O formato de pixels da imagem não é suportado por este método.");
            Graphics graphicsImage = Graphics.FromImage(bitmap);
            graphicsImage.SmoothingMode = SmoothingMode.HighQuality;
            graphicsImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphicsImage.DrawImage(SourceImage, 0, 0, bitmap.Width, bitmap.Height);
            graphicsImage.Dispose();
            return (Image)bitmap;
        }

        /// <summary>
        /// Redimensionar este <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="SourceImage"> este <see cref="Bitmap"/>.</param>
        /// <param name="NewHeight">Altura desejada.</param>
        /// <param name="NewWidth">Largura desejada.</param>
        /// <returns></returns>
        public static Bitmap BitmapResize(this Bitmap SourceImage, Int32 NewHeight, Int32 NewWidth)
        {
            Bitmap bitmap = new Bitmap(NewWidth, NewHeight, SourceImage.PixelFormat);
            if (bitmap.PixelFormat == PixelFormat.Format1bppIndexed | bitmap.PixelFormat == PixelFormat.Format4bppIndexed | bitmap.PixelFormat == PixelFormat.Format8bppIndexed | bitmap.PixelFormat == PixelFormat.Undefined | bitmap.PixelFormat == PixelFormat.DontCare | bitmap.PixelFormat == PixelFormat.Format16bppArgb1555 | bitmap.PixelFormat == PixelFormat.Format16bppGrayScale)
                throw new NotSupportedException("O formato de pixels da imagem não é suportado por este método.");
            Graphics graphicsImage = Graphics.FromImage(bitmap);
            graphicsImage.SmoothingMode = SmoothingMode.HighQuality;
            graphicsImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphicsImage.DrawImage(SourceImage, 0, 0, bitmap.Width, bitmap.Height);
            graphicsImage.Dispose();
            return (Bitmap)bitmap;
        }

        /// <summary>
        /// Redimensionar a imagem contida neste <see cref="Stream"/> no <see cref="Stream"/> de saída.
        /// </summary>
        /// <param name="fromStream">Este <see cref="Stream"/>.</param>
        /// <param name="scaleFactor">Fator de redimensionamento.</param>
        /// <param name="toStream"><see cref="Stream"/> de saída.</param>
        public static void ResizeImage(this Stream fromStream, double scaleFactor, Stream toStream)
        {
            var image = Image.FromStream(fromStream);
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var abort = new Image.GetThumbnailImageAbort(ThumbnailCallback);
            var thumbnail = image.GetThumbnailImage(newWidth, newHeight, abort, IntPtr.Zero);
            thumbnail.Save(toStream, image.RawFormat);
            thumbnail.Dispose();
            image.Dispose();
        }

        /// <summary>
        /// Suporte de callback para redimensionamento via <see cref="Stream"/>.
        /// </summary>
        /// <returns></returns>
        private static bool ThumbnailCallback()
        { return false; }

        /// <summary>
        /// Redimensionar a imagem que esteja neste <see cref="Stream"/> para o <see cref="Stream"/> de saída.
        /// </summary>
        /// <param name="fromStream"></param>
        /// <param name="toStream"></param>
        /// <param name="scaleFactor"></param>
        public static void ResizeImage(Stream fromStream, Stream toStream, double scaleFactor)
        {
            var image = Image.FromStream(fromStream);
            var newWidth = (int)(image.Width * scaleFactor);
            var newHeight = (int)(image.Height * scaleFactor);
            var thumbnailBitmap = new Bitmap(newWidth, newHeight);
            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);
            thumbnailBitmap.Save(toStream, image.RawFormat);
            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }

        /// <summary>
        /// Gerar um thumb da imagem
        /// </summary>
        /// <param name="bte"></param>
        /// <param name="thumbWidth"></param>
        /// <param name="thumbHeight"></param>
        /// <returns></returns>
        public static byte[] MakeThumbnail(this byte[] bte, int thumbWidth, int thumbHeight)
        {
            using MemoryStream ms = new();
            using Image thumbnail = Image.FromStream(new MemoryStream(bte)).GetThumbnailImage(thumbWidth, thumbHeight, null, new IntPtr());
            thumbnail.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
