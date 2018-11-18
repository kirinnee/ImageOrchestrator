using Minimage;

namespace Kirinnee.ImageOrchestrator.Transformers
{
    public class PngQuantTransformer: ImageTransformer
    {
        
        private readonly PngQuant _pngquant;
        
        public PngQuantTransformer(PngQuantOptions options = null)
        {
            _pngquant = new PngQuant(options);
        }


        public override byte[] Transform(byte[] input)
        {
            return _pngquant.Compress(input).Result;
        }
    }
}