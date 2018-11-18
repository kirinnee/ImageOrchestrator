using Minimage;

namespace Kirinnee.ImageOrchestrator.Transformers
{
    public class WuQuantTransformer: ImageTransformer
    {
        private readonly WuQuant _transformer;

        public WuQuantTransformer()
        {
            _transformer = new WuQuant();
        }
        
        public override byte[] Transform(byte[] input)
        {
            return _transformer.Compress(input).Result;
        }
    }
}