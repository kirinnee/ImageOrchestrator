# Image Orchestrator

.NET Standard Wrapper to allow for programmatic call for both OptiPNG and JPEGOptim to compress image. 
There is an addition nQuant PNG Quantizer which does not require the binary to be installed.

All Compression methods are *lossy*.

Allows piping for resizing, VirtualImage object etc.

# Prerequisite

*All dependency needs to be avaliable via `${PATH}`*

[PNGQuant](https://pngquant.org/) - PNG Compression Tool. Minimum version 2.12.0

[JPEGOptim](https://github.com/tjko/jpegoptim) - JPEG Compression Tool. Minimum version 1.4.4

**Ensure Binary version are up to date!**

Check PNGQuant version :
```shell
$ pngquant --verbose
```

Check JPEGOptim version :
```shell
$ jpegoptim --help
```

# Getting Started

Install via .NET CLI
```powershell
$ dotnet add package Kirinnee.Minimage 
```

or 

Install via NuGet Package Manager
```powershell
PM> Install-Package Kirinnee.Minimage 
```

# Features

## Transformer
Create a transformer which dictates what transformation (resize, compress etc) to apply to the images.


### JpegOptimTransformer

**Without settings**
```cs
ImageTransform jpegoptim = new JpegOptimTrasnformer();
```
**With Settings**
```cs
var options = new JpegOptimOptions(){
    Quality = 80, //Maximum quality, default 76
    StripAll = true, //Strip all metadata and markers, default true
    Overwrite = false, //Overwrite target file if it already exist, default false
    Progressive = false, //Make the image progressive. Will make the image not progessive if false. default true,
    Force = true //Compress regardless of 
};
///Invoke the compressor
ImageTransform jpegoptim = new JpegOptimTrasnformer(options);
```

### PngQuantTransformer

**Without Settings**
```cs
ImageTransform pngquant = new PngQuantTransformer();
```
**With Settings**
```cs
//Options
var options = new PngQuantOptions(){
    QualityMinMax = (65,80), //Minimum = 65, Maximum = 80. Default null
    Spped = 1, //Value between 1 and 11. default 3. 
    IEBug = false, //Attempt to fix iebug. default false.
    Bit = 256 //bit-rate. default 256
};
ImageTransform pngquant = new PngQuantTransformer(options);
```

### WuQuant Transformer

High-speed quantizer, using  Xialoin Wu's fast optimal color quantizer with nQuant. Image quality drops quite significantly, but runs really fast.

```cs
ImageTransform wuquant = new WuQuantTransformer();
```

### Resize Transformer

Resize the image if the bytes matches the MimeType.

Unsupported MimeTypes will result in error thrown.

Supported MimeTypes:
- `image/png`
- `image/jpeg`
- `image/bmp`
- `image/gif`
- `image/tiff`
- `image/x-icon` 
```cs
//Resize to 50% of width and height if mimetype is image/png or image/jpeg
ImageTransform resize = new PercentageResizeTransformer(0.5, new[]{"image/png","image/jpeg"});
```

## Using Transformers 

Here, I will use `wuquant` and `resize` object as examples. Refer to above as to how they are created.

### Traditional Usage with byte array
```cs
//Image
byte[] originalImage = File.ReadAllBytes("image.png");

//Trandition object invoke
var quantized = wuquant.Transform(originalImage);
var resized = resize.Transform(originalImage);

//Linq Chaining
var output = originalImage.Transform(wuqaunt).Transform(resize);
```

### Tradition Usage with Virtual Image
```cs
//Arguments: Absolute Path to root of image folder (for context), absolute path to read, absolute to write
VirtualImage image = new VirtualImage("from","from/image.png","to/image.png");
//Reads the files into the object
image.Read();

var output = image.Transform(wuquant).Transform(resize);
//Write to output destination
output.Write();
//Or use it content for something
byte[] images = output.Content;
```

### Piping Operator with byte array
```cs
byte[] originalImage = File.ReadAllBytes("image.png");
var output = originalImage | wuquant | resize | jepgoptim; 
```

### Piping Operator with Virtual Image
```cs
//Arguments: Absolute Path to root of image folder (for context), absolute path to read, absolute to write
VirtualImage image = new VirtualImage("from","from/image.png","to/image.png");
//Reads the files into the object
image.Read();
//Get Transformed virtual image
var output = image | wuquant | resize | jpegoptim;

```

## Contributing
Please read [CONTRIBUTING.md](CONTRIBUTING.MD) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning 
We use [SemVer](https://semver.org/) for versioning. For the versions available, see the tags on this repository.

## Authors
* [kirinnee](mailto:kirinnee@gmail.com) 

## License
This project is licensed under MIT - see the [LICENSE.md](LICENSE.MD) file for details