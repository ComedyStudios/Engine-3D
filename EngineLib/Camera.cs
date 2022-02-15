using System.Diagnostics;
using System.Numerics;

namespace EngineLib;

public class Camera: SceneObject
{
    /// <summary>
    /// Creates an instance of the Camera
    /// </summary>
    /// <param name="x">x axis of the camera position.</param>
    /// <param name="y">y axis of the camera position.</param>
    /// <param name="z">x axis of the camera position.</param>
    /// <param name="fov">field of view of the cammera</param>
    /// <param name="width">x pixels of the cam</param>
    /// <param name="height">y pixels of the cam</param>
    public Camera(float x, float y, float z, float fov, int width, int height): base(x,y,z)
    {
        Fov = fov;
        Width = width;
        Height = height;
    }
    /// <summary>
    /// Camera resolution - width
    /// </summary>
    public float Width { get; }
    
    /// <summary>
    /// Camera resolution - width
    /// </summary>
    public float Height { get; }

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
        var width = Width;
        var height = Height;
        var fov = Fov;
        var aspectRatio =  AspectRatio;

        var pixelCameraX = (float)((2f * (x + 0.5f) / width - 1f)* aspectRatio * Math.Tan(fov*Math.PI/180/2));
        var pixelCameraY = (float)(1f - 2f * ((y + 0.5f) / height)* Math.Tan(fov/2*Math.PI/ 180));
        var cameraSpace = new Vector3(pixelCameraX, pixelCameraY, 1);
        
        var worldCoordinate = cameraSpace.LocalToGlobalCoordinate(this);
        worldCoordinate = Vector3.Normalize(worldCoordinate-Position);
        
        return worldCoordinate;
    }

    public void MoveCamera(Input input)
    {
        var angularSpeed = 1;
        var translationVector = new Vector3(input.HorizontalMovement, 0, input.VerticalMovement) * (float)input.DeltaTime * input.MovementSpeed;
        MoveObject(translationVector);
        Rotate((float)input.MouseDeltaY * angularSpeed, (float)input.MouseDeltaX * angularSpeed, 0);
        Trace.WriteLine(input.MouseDeltaY);
    }
}