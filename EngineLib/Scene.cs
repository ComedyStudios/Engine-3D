using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

namespace EngineLib;

public class Scene
{
    private readonly Vector3 WorldOrigin ;
    private Sphere s1;
    private Sphere s2;
    
        
    /// <summary>
    /// constructor of the class, determines all objects in the scene and puts them in a list
    /// </summary>
    public Scene()
    {
        WorldOrigin = new Vector3(0, 0, 0);
        MainCamera = new Camera(0, 0,  -20);
        s1  = new Sphere(0, 0,10,2, Color.Red);
        s2 = new Sphere(20, 0, 9, 5, Color.LimeGreen);
        Objects.Add(s1);
        Objects.Add(s2);
    }
    
    /// <summary>
    /// camera of the Scene
    /// </summary>
    public Camera MainCamera { get; set; }
    
    /// <summary>
    /// Array with all the objects in the Scene
    /// </summary>
    public List<SceneObject> Objects { get; set; } = new List<SceneObject>();
    
    /// <summary>
    /// converts 2D cordinates of a pixel into a direction of a ray
    /// </summary>
    /// <param name="x">coordinate of the pixel</param>
    /// <param name="y">coordinate of the pixel</param>
    /// <param name="camera">camera the ray is being cast from</param>
    /// <returns>direction</returns>
    public Vector3 CamToWorldCordinate(int x, int y, Camera camera)
    {
        var width = Camera.Width;
        var height = Camera.Height;
        var fov = camera.Fov;
        var aspectRatio =  camera.AspectRatio;

        var pixelCameraX = (float)((2f * (x + 0.5f) / width - 1f)* aspectRatio * Math.Tan(fov*Math.PI/180/2));
        var pixelCameraY = (float)(1f - 2f * ((y + 0.5f) / height)* Math.Tan(fov/2*Math.PI/ 180));
        var cameraSpace = new Vector3((float)pixelCameraX, (float)pixelCameraY, 1);
        
        var worldCoordinate = Utils.LocalToGlobalCoordinate(cameraSpace, camera, WorldOrigin);
        worldCoordinate = Vector3.Normalize(worldCoordinate-camera.Position);
        
        return worldCoordinate;
    }
}