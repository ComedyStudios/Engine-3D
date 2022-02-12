﻿using System.Drawing;
using System.Numerics;

namespace EngineLib;

public class Scene
{
    //creating world objects
    private Sphere s1;
    private Sphere s2;
    private Plane p1;
    
    //Creating Light-sources
    private Lightsource l2;
    private Lightsource l1;
    
    
        
    /// <summary>
    /// constructor of the class, determines all objects in the scene and puts them in a list
    /// </summary>
    public Scene()
    {
        MainCamera = new Camera(0, 0,  -20, 90);
        p1 = new Plane(-50, -4, -20, 100, 100, Color.NavajoWhite,1, 0.5f);
        s1  = new Sphere(-10, 0,0,3, Color.Chocolate, 1, 0.25f);
        s2 = new Sphere(10, 0, 0, 2, Color.LimeGreen, 1,0);
        
        Objects.Add(s1);
        Objects.Add(s2);
        Objects.Add(p1);

        l1 = new Lightsource(0, 10, 0, 30, Color.White);
        l2 = new Lightsource(0, 3, -10, 20, Color.White);
        Lightsources.Add(l2);
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
    
}