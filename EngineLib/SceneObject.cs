using System.Drawing;
using System.Numerics;

namespace EngineLib;

public abstract class SceneObject
{
    /// <summary>
    /// position and Axis of the Object
    /// </summary>
    public Vector3 Position;
    public Vector3 XAxis = new Vector3(1, 0, 0);
    public Vector3 YAxis= new Vector3(0, 1, 0);
    public Vector3 ZAxis = new Vector3(0, 0, 1);

    /// <summary>
    /// applies shading to a certain pixel
    /// </summary>
    /// <param name="position">position where the shading takes place</param>
    /// <param name="lightSource">the light-source that is being checked</param>
    /// <param name="color">the Original color of the Sphere</param>
    /// <param name="normal">the Normal Vector at the pixel</param>
    /// <returns>new Color with the Shading applied</returns>
        protected Color Shading(Vector3 position, List<Lightsource> lightSources, Color color, Vector3 normal, float albedo)
        {
            var finalColor = Color.Black;
            
            foreach (var lightSource in lightSources)
            {
                //calculate values
                var lightDir = Vector3.Normalize(lightSource.Position - position);
                var lightDot = Math.Max(Vector3.Dot(lightDir,normal), 0);
                var lightReflected = albedo / Math.PI;
                var lightPower = lightDot * lightSource.Intensity;
                //TODO: check for shadows
                
                var newColor = calculateColorValue(color, lightPower, lightReflected);
                finalColor = AddColors(finalColor, newColor);
            }

            return finalColor;
        }

        private Color calculateColorValue(Color colorValue, float lightPower, double lightReflected)
        {
            var r = ((float)colorValue.R / 255) * lightPower * lightReflected;
            var g = ((float)colorValue.G / 255) * lightPower * lightReflected;
            var b = ((float)colorValue.B / 255) * lightPower * lightReflected;
            return Color.FromArgb(Math.Min((int)(r * 255), 255), Math.Min((int)(g * 255), 255), Math.Min((int)(b * 255), 255));
        }


        private static Color AddColors(Color color1, Color color2)
        {
            return Color.FromArgb(Math.Min(255, color1.R + color2.R), Math.Min(255, color1.G + color2.G), Math.Min(255, color1.B + color2.B));
        }
    }