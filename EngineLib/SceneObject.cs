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
    protected Color Shading(Vector3 position, Lightsource lightSource, Color color, Vector3 normal)
    {
        var lightDir = Vector3.Normalize(lightSource.Position - position);
        var lightFactor = Math.Max(Vector3.Dot(lightDir,normal), 0);
        var newColor = Color.FromArgb((int)(color.R * lightFactor), (int)(color.G * lightFactor), (int)(color.B * lightFactor));
        return newColor;
    }
}