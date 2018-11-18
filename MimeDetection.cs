using System;
using System.Drawing.Imaging;

namespace Kirinnee.ImageOrchestrator
{
    public static class MimeDetection
    {
        public static ImageFormat GetImageFormatFromMime(this string mimeType)
        {
            switch (mimeType)
            {
                case "image/png": return ImageFormat.Png;
                case "image/jpeg": return ImageFormat.Jpeg;
                case "image/bmp": return ImageFormat.Bmp;
                case "image/gif": return ImageFormat.Gif;
                case "image/x-icon": return ImageFormat.Icon;
                case "image/tiff": return ImageFormat.Tiff;
                case "application/x-msmetafile": return ImageFormat.Wmf;
                case "image/x-emf": return ImageFormat.Emf;
                default : throw new FormatException("Unknown image mime type: "+ mimeType);
            }
        }
        
        
        
    }
}