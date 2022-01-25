﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

namespace Engine_3D;

public class Scene
{
    public Cam mainCam;
    public Vector3 WorldOrigin;
    public List<SceneObject> Objects = new List<SceneObject>();
    private Sphere s1;


    public Scene()
    {
        mainCam = new Cam(0, 0, -10);
        s1  = new Sphere(0, 0,0, 1);
        Objects.Add(s1);
    }
    
    public Vector3 camToWorldCordinate(int x, int y, Cam cam)
    {
        //TODO: The vectro is pointing int the oposite direction 
        var width = cam.WindowWidth;
        var height = cam.WindowHeight;
        var fov = cam.fov;
        var AspectRatio =  (float)width / (float)height;

        var PixelCameraX = (float)(2f * ((x + 0.5f) / width) - 1f* AspectRatio * Math.Tan(fov/2*Math.PI/ 180));
        var PixelCameraY = (float)(1f - 2f * ((y + 0.5f) / height)* Math.Tan(fov/2*Math.PI/ 180));

        var CameraSpace = new Vector3((float)PixelCameraX, (float)PixelCameraY, 1);

        var Translation = cam.center - WorldOrigin;
        var WorldCoordinate = new Vector3(
            cam.XAxis.X * CameraSpace.X + cam.YAxis.X * CameraSpace.Y + cam.ZAxis.X * CameraSpace.Z + Translation.X,
            cam.XAxis.Y * CameraSpace.X + cam.YAxis.Y * CameraSpace.Y + cam.ZAxis.Y * CameraSpace.Z + Translation.Y,
            cam.XAxis.Z * CameraSpace.X + cam.YAxis.Z * CameraSpace.Y + cam.ZAxis.Z * CameraSpace.Z + Translation.Z);
        WorldCoordinate = Vector3.Normalize(WorldCoordinate);
        return -WorldCoordinate;
    }
}