using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

namespace EngineLib;

public class Scene
{
    private Vector3 WorldOrigin;
    private Sphere s1;
    
    public Scene()
    {
        WorldOrigin = new Vector3(0, 0, 0);
        //TODO: ray-sphere colision doesnt work at all if camera coordinates are negative
        mainCam = new Cam(0, 0,  -10);
        s1  = new Sphere(0, 0,10,1);
        Objects.Add(s1);
    }
    
    public Cam mainCam { get; set; }
    public List<SceneObject> Objects { get; set; } = new List<SceneObject>();
    
    public Vector3 CamToWorldCordinate(int x, int y, Cam cam)
    {
        var width = cam.WindowWidth;
        var height = cam.WindowHeight;
        var fov = cam.fov;
        var AspectRatio =  (float)width / (float)height;

        var PixelCameraX = (float)((2f * (x + 0.5f) / width - 1f)* AspectRatio * Math.Tan(fov*Math.PI/180/2));
        var PixelCameraY = (float)(1f - 2f * ((y + 0.5f) / height)* Math.Tan(fov/2*Math.PI/ 180));

        var CameraSpace = new Vector3((float)PixelCameraX, (float)PixelCameraY, 1);

        //TODO: this matrix is to retarded to convert to values if the cam locaiton is negative
        var Translation = cam.Position - WorldOrigin;
        var WorldCoordinate = new Vector3(
            cam.XAxis.X * CameraSpace.X + cam.YAxis.X * CameraSpace.Y + cam.ZAxis.X * CameraSpace.Z + Translation.X,
            cam.XAxis.Y * CameraSpace.X + cam.YAxis.Y * CameraSpace.Y + cam.ZAxis.Y * CameraSpace.Z + Translation.Y,
            cam.XAxis.Z * CameraSpace.X + cam.YAxis.Z * CameraSpace.Y + cam.ZAxis.Z * CameraSpace.Z + Translation.Z);
        WorldCoordinate = Vector3.Normalize(WorldCoordinate-cam.Position);
        
        return WorldCoordinate;
    }
}