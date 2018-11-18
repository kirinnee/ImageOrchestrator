using System.Linq;
using Kirinnee.ImageOrchestrator.Transformers;

namespace Kirinnee.ImageOrchestrator
{
    internal static class Utility
    {
        internal static int Percentage(this int input, double percentage)
        {
            return (int) ((double) input * percentage);
        }
    }


    public static class TransformerChainer
    {
        /// <summary>
        /// Transform the input byte with the image transformer
        /// </summary>
        /// <param name="input"></param>
        /// <param name="transformer"></param>
        /// <returns></returns>
        public static byte[] Transform(this byte[] input, ImageTransformer transformer)
        {
            return transformer.Transform(input);
        }

        /// <summary>
        /// Transforms all input bytes with the image transformer in parallel
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="transformer"></param>
        /// <returns></returns>
        public static byte[][] Transform(this byte[][] inputs, ImageTransformer transformer)
        {
            return inputs.AsParallel().Select(s => s.Transform(transformer)).ToArray();
        }

        /// <summary>
        /// Transform all input virtual image with image transformer in parallel
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="transformer"></param>
        /// <returns></returns>
        public static VirtualImage[] Transform(this VirtualImage[] inputs, ImageTransformer transformer)
        {
            return inputs.AsParallel().Select(s => s.Transform(transformer)).ToArray();
        }
    }
}