using System.Numerics;

namespace EngineLib;

public class Camera: SceneObject
{
    /// <summary>
    /// Camera resolution - width
    /// </summary>
    public const float Width = 1280;
    
    /// <summary>
    /// Camera resolution - width
    /// </summary>
    public const float Height = 720;
    
    /// <summary>
    /// Creates an instance of the Camera
    /// </summary>
    /// <param name="x">x axis of the camera position.</param>
    /// <param name="y">y axis of the camera position.</param>
    /// <param name="z">x axis of the camera position.</param>
    public Camera(float x, float y, float z)
    {
        Fov = 90;
        Position = new Vector3(x, y, z);;
    }

    /// <summary>
    /// Gets or set camera field of view
    /// </summary>
    public float Fov { get; set; }
    
    /// <summary>
    /// Get camera aspect ratio
    /// </summary>
    public float AspectRatio => Width / Height;
}