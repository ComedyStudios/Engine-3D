using System.Drawing;
using System.Numerics;

namespace EngineLib;

public abstract class SceneObject
{
    protected SceneObject(float x, float y, float z, float albedo, float reflectivity, Color color)
    {
        Position = new Vector3(x, y, z);
        Albedo = albedo;
        Reflectivity = reflectivity;
        Color = color;
    }

    protected SceneObject(float x, float y, float z, Color color)
    {
        Position = new Vector3(x, y, z);
        Color = color;
    }

    protected SceneObject(float x, float y, float z)
    {
        Position = new Vector3(x, y, z);
    }
    
    /// <summary>
    /// position and Axis of the Object
    /// </summary>
    public Vector3 Position;
    public Vector3 XAxis = new(1, 0, 0);
    public Vector3 YAxis= new(0, 1, 0);
    public Vector3 ZAxis = new(0, 0, 1);
    
    /// <summary>
    /// Reflectiveness of the Sphere
    /// </summary>
    public float Albedo { get; set; }
    public float Reflectivity { get; set;}
    
    /// <summary>
    /// Color of the Object
    /// </summary>
    public Color Color { get; set; }
}