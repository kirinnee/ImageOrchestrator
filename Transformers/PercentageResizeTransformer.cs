using System.Drawing;
using System.IO;
using System.Linq;
using MimeDetective;

namespace Kirinnee.ImageOrchestrator.Transformers
{
    public class PercentageResizeTransformer : ImageTransformer
    {
        private readonly double _percentage;
        private readonly string[] _mimeTypes;

        public PercentageResizeTransformer(double percentage, string[] acceptedMimeType)
        {
            _percentage = percentage;
            _mimeTypes = acceptedMimeType;
        }

        public override byte[] Transform(byte[] input)
        {
            var mime = input.GetFileType().Mime;
            if (!_mimeTypes.Contains(mime)) return input;
            
            using(MemoryStream stream = new MemoryStream(input), outStream = new MemoryStream())
            using (var bitmap = new Bitmap(stream))
            using(var output = new Bitmap(bitmap, new Size(bitmap.Width.Percentage(_percentage), bitmap.Height.Percentage(_percentage))))
            {
                output.Save(outStream, mime.GetImageFormatFromMime());
                return outStream.ToArray();
            }
        }
        
        
    }
}