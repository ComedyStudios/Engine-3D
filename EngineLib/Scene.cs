using System.Drawing;
using System.Numerics;

namespace EngineLib;

public class Scene
{

    //creating world objects
    private Sphere s1;
    private Sphere s2;
    private Plane p1;
    
    //Creating Light-sources
    private Lightsource l1;
    
    
        
    /// <summary>
    /// constructor of the class, determines all objects in the scene and puts them in a list
    /// </summary>
    public Scene()
    {
        MainCamera = new Camera(0, 0,  -20);
        p1 = new Plane(-50, -10, -20, 100, 100, Color.NavajoWhite);
        s1  = new Sphere(-5, 0,1,2, Color.DarkRed);
        s2 = new Sphere(5, 0, 1, 2, Color.LimeGreen);
        
        Objects.Add(s1);
        Objects.Add(s2);
        Objects.Add(p1);

        l1 = new Lightsource(0, 3, -4, 1000);
        Lightsources.Add(l1);
    }
    
    /// <summary>
    /// camera of the Scene
    /// </summary>
    public Camera MainCamera { get; set; }

    /// <summary>
    /// List with all Light-sources in the Scene 
    /// </summary>
    public List<Lightsource> Lightsources { get; set; } = new List<Lightsource>();

    /// <summary>
    /// Array with all the objects in the Scene
    /// </summary>
    public List<SceneObject> Objects { get; set; } = new List<SceneObject>();
    
    /// <summary>
    /// converts 2D coordinates of a pixel into a direction of a ray
    /// </summary>
    /// <param name="x">coordinate of the pixel</param>
    /// <param name="y">coordinate of the pixel</param>
    /// <param name="camera">camera the ray is being cast from</param>
    /// <returns>direction</returns>
    public Vector3 CameraToWorldCoordinate(int x, int y, Camera camera)
    {
        var width = Camera.Width;
        var height = Camera.Height;
        var fov = camera.Fov;
        var aspectRatio =  camera.AspectRatio;

        var pixelCameraX = (float)((2f * (x + 0.5f) / width - 1f)* aspectRatio * Math.Tan(fov*Math.PI/180/2));
        var pixelCameraY = (float)(1f - 2f * ((y + 0.5f) / height)* Math.Tan(fov/2*Math.PI/ 180));
        var cameraSpace = new Vector3(pixelCameraX, pixelCameraY, 1);
        
        var worldCoordinate = Utils.LocalToGlobalCoordinate(cameraSpace, camera);
        worldCoordinate = Vector3.Normalize(worldCoordinate-camera.Position);
        
        return worldCoordinate;
    }
}