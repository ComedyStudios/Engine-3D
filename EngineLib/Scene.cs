using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

namespace EngineLib;

public class Scene
{
    private Vector3 WorldOrigin;
    private Sphere s1;
    private Sphere s2;
    
    //TODO: multiple Spheres cant be shown for some reason
    public Scene()
    {
        WorldOrigin = new Vector3(0, 0, 0); 
        MainCamera = new Camera(0, 0,  -20);
        //s1  = new Sphere(0, 0,10,2, Color.Red);
        s2 = new Sphere(20, 0, 9, 5, Color.LimeGreen);
       // Objects.Add(s1);
        Objects.Add(s2);
    }
    
    public Camera MainCamera { get; set; }
    public List<SceneObject> Objects { get; set; } = new List<SceneObject>();
    
    public Vector3 CamToWorldCordinate(int x, int y, Camera camera)
    {
        var width = Camera.Width;
        var height = Camera.Height;
        var fov = camera.Fov;
        var aspectRatio =  camera.AspectRatio;

        var pixelCameraX = (float)((2f * (x + 0.5f) / width - 1f)* aspectRatio * Math.Tan(fov*Math.PI/180/2));
        var pixelCameraY = (float)(1f - 2f * ((y + 0.5f) / height)* Math.Tan(fov/2*Math.PI/ 180));
        var cameraSpace = new Vector3((float)pixelCameraX, (float)pixelCameraY, 1);

        //TODO: this matrix is to retarded to convert to values if the cam locaiton is negative
        var translation = camera.Position - WorldOrigin;
        var worldCoordinate = new Vector3(
            camera.XAxis.X * cameraSpace.X + camera.YAxis.X * cameraSpace.Y + camera.ZAxis.X * cameraSpace.Z + translation.X,
            camera.XAxis.Y * cameraSpace.X + camera.YAxis.Y * cameraSpace.Y + camera.ZAxis.Y * cameraSpace.Z + translation.Y,
            camera.XAxis.Z * cameraSpace.X + camera.YAxis.Z * cameraSpace.Y + camera.ZAxis.Z * cameraSpace.Z + translation.Z);
        worldCoordinate = Vector3.Normalize(worldCoordinate-camera.Position);
        
        return worldCoordinate;
    }
}