using System.Drawing;

namespace EngineLib;

public static class ColorExtensions
{
    /// <summary>
    /// Multiplies color2 with a factor
    /// </summary>
    /// <param name="color1">Color</param>
    /// <param name="factor">a factor between 0 and 1</param>
    /// <returns>the new Color</returns>
    public static Color ColorMultiply( this Color color1, double factor)
    {
        var r = ((float)color1.R / 255) * factor;
        var g = ((float)color1.G / 255) * factor;
        var b = ((float)color1.B / 255) * factor;
        return Color.FromArgb(Math.Min((int)(r * 255), 255), Math.Min((int)(g * 255), 255), Math.Min((int)(b * 255), 255));
    }
        
    /// <summary>
    /// Multiplies a color2 with another color2
    /// </summary>
    /// <param name="color1">first color</param>
    /// <param name="color2">second color</param>
    /// <returns></returns>
    public static Color ColorMultiply(this Color color1, Color color2)
    {
        var r = ((float)color1.R / 255) * ((float)color2.R / 255);
        var g = ((float)color1.G / 255) * ((float)color2.G / 255);
        var b = ((float)color1.B / 255) * ((float)color2.B / 255);
        return Color.FromArgb(Math.Min((int)(r * 255), 255), Math.Min((int)(g * 255), 255), Math.Min((int)(b * 255), 255));
    }

    /// <summary>
    /// Adds the value of two Colors
    /// </summary>
    /// <param name="color1">first color</param>
    /// <param name="color2">second color</param>
    /// <returns>color</returns>
    public static Color AddColor(this Color color1, Color color2)
    {
        return Color.FromArgb(Math.Min(255, color1.R + color2.R), Math.Min(255, color1.G + color2.G), Math.Min(255, color1.B + color2.B));
    }
}