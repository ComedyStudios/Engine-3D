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
    /// <param name="fov">field of view of the cammera</param>
    public Camera(float x, float y, float z, float fov): base(x,y,z)
    {
        Fov = fov;
    }

    /// <summary>
    /// Gets or set camera field of view
    /// </summary>
    public float Fov { get; set; }
    
    /// <summary>
    /// Get camera aspect ratio
    /// </summary>
    public float AspectRatio => Width / Height;
    
    /// <summary>
    /// Transforms Pixel cordinates to WorldCordinates
    /// </summary>
    /// <param name="x">vertical coordinate of Pixel</param>
    /// <param name="y">horizontal coordinate of Pixel</param>
    /// <returns>Position of pixel in scene</returns>
    public Vector3 CameraToWorldCoordinate(int x, int y)
    {
        var width = Camera.Width;
        var height = Camera.Height;
        var fov = Fov;
        var aspectRatio =  AspectRatio;

        var pixelCameraX = (float)((2f * (x + 0.5f) / width - 1f)* aspectRatio * Math.Tan(fov*Math.PI/180/2));
        var pixelCameraY = (float)(1f - 2f * ((y + 0.5f) / height)* Math.Tan(fov/2*Math.PI/ 180));
        var cameraSpace = new Vector3(pixelCameraX, pixelCameraY, 1);
        
        var worldCoordinate = cameraSpace.LocalToGlobalCoordinate(this);
        worldCoordinate = Vector3.Normalize(worldCoordinate-Position);
        
        return worldCoordinate;
    }
}