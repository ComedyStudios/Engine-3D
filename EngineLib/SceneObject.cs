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
    /// Reflectiveness of the Sphere
    /// </summary>
    public float Albedo { get; set; }
}