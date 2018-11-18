using System.Linq;

namespace Kirinnee.ImageOrchestrator.Transformers
{
    public abstract class ImageTransformer
    {
        public abstract byte[] Transform(byte[] input);

        public static byte[] operator |(byte[] input, ImageTransformer transformer)
        {
            return transformer.Transform(input);
        }

        public static byte[][] operator |(byte[][] input, ImageTransformer transformer)
        {
            return input.AsParallel().Select(transformer.Transform).ToArray();
        }

        public static VirtualImage[] operator |(VirtualImage[] input, ImageTransformer transformer)
        {
            return input.AsParallel().Select(vi => vi.Transform(transformer)).ToArray();
        }

        public static VirtualImage operator |(VirtualImage input, ImageTransformer trans)
        {
            return input.Transform(trans);
        }
    }
}