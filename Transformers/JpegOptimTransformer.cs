using Minimage;

namespace Kirinnee.ImageOrchestrator.Transformers
{
    public class JpegOptimTransformer:  ImageTransformer
    {

        private readonly JpegOptim _optim;
        
        public JpegOptimTransformer(JpegOptimOptions options = null)
        {
            _optim = new JpegOptim(options);
        }
        
        public override byte[] Transform(byte[] input)
        {
            return _optim.Compress(input).Result;
        }
    }
}