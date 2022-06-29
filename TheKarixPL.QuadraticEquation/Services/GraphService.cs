using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace TheKarixPL.QuadraticEquation.Services;

/// <summary>
/// Service which generates graph image
/// </summary>
public class GraphService
{
    /// <summary>
    /// Generate graph and save it to image
    /// </summary>
    /// <param name="a">a</param>
    /// <param name="b">b</param>
    /// <param name="c">c</param>
    /// <param name="height">Height of image</param>
    /// <param name="width">Width of image</param>
    /// <param name="output">Stream to write image</param>
    public void GenerateGraph(double a, double b, double c, int height, int width, Stream output)
    {
        using var image = new Image<Rgba32>(width, height);
        for (int i = 0; i < height; i++)
            image[width / 2, i] = Color.Black.ToPixel<Rgba32>();
        for (int i = 0; i < width; i++)
            image[i, height / 2] = Color.Black.ToPixel<Rgba32>();

        var equation = Library.QuadraticEquation.FromStandardForm(a, b, c);
        var pb = new PathBuilder();
        for (int i = 0; i < width; i++)
        {
            var x = i - width / 2;
            var y = (int)equation.GetValue(x);
            var y2 = (int)equation.GetValue(x + 1);
            if (y + height / 2 < height)
                pb.AddLine(new PointF(x + width / 2, y + height / 2), new PointF(x + 1 + width / 2, y2 + height / 2));
            image.Mutate(img => img.Draw(Color.Black, 1, pb.Build()));
        }
        
        image.SaveAsPng(output);
    }
}