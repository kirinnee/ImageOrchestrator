using System;
using System.IO;
using Kirinnee.ImageOrchestrator.Transformers;

namespace Kirinnee.ImageOrchestrator
{
    public class VirtualImage
    {

        /// <summary>
        /// Absolute from path
        /// </summary>
        public readonly string From;
        /// <summary>
        /// Absolute to path
        /// </summary>
        public readonly string To;
        /// <summary>
        /// Absolute path to the source folder, as context to generate relative paths.
        /// </summary>
        public readonly string SrcContext;
        public byte[] Content { get; private set; }
        
        /// <summary>
        /// Creates a virtual image
        /// </summary>
        /// <param name="srcContext">The source folder. Used to calculate source relative path</param>
        /// <param name="from">The absolute path to read from</param>
        /// <param name="to">The absolute path to write to</param>
        public VirtualImage(string srcContext, string from, string to)
        {
            SrcContext = srcContext;
            From = from ?? throw new ArgumentNullException(nameof(from));
            To= to ?? throw new ArgumentNullException(nameof(to));
            Content = new byte[] { };
        }
        
        /// <summary>
        /// Read the image. 
        /// </summary>
        /// <returns></returns>
        public VirtualImage Read()
        {
            Content = File.ReadAllBytes(From);
            return this;
        }

        /// <summary>
        /// Write file content to To path
        /// </summary>
        /// <returns></returns>
        public VirtualImage Write()
        {
            File.WriteAllBytes(To, Content);
            return this;
        }

        /// <summary>
        /// Transform the image bytes using a transformer
        /// </summary>
        /// <param name="transformer"></param>
        /// <returns></returns>
        public VirtualImage Transform(ImageTransformer transformer)
        {
            Content = transformer.Transform(Content);
            return this;
        }
    }
}