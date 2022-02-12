using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Windows.Media.Media3D;

namespace EngineLib;

public class Sphere : SceneObject, IVisible
{
    /// <summary>
    /// Constructro of the class
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="z">z coordinate</param>
    /// <param name="radius">radius of the Sphere</param>
    /// <param name="color">color of the Sphere</param>
    /// <param name="albedo">reflectiveness from 0 to 1 </param>
    /// <param name="reflectivity">the reflectivity of the Sphere</param>
    public Sphere(float x, float y,float z, float radius, Color color, float albedo, float reflectivity): base(x, y, z, albedo,reflectivity, color)
    {
        Radius = radius;
    }
    
    
    
    /// <summary>
    /// Radius of the Sphere
    /// </summary>
    public float Radius { get; set; }
    

    /// <summary>
    /// checks if the ray hit this Object and applies shading 
    /// </summary>
    /// <param name="ray">Ray that is being checked </param>  
    /// <param name="scene">Scene the Object ist in</param>
    /// <returns>Returns information about the hit</returns>
    public RayHit? RayCastHit(Ray ray, Scene scene)
    {
        var transform = Position - ray.Origin;
        var projection = Vector3.Dot(transform, ray.Direction);
        var temp = Vector3.Dot(transform, transform);
        var distance = Math.Sqrt(temp- projection * projection);
        
        //check if the Ray hit the Object
        if (distance < Radius && !(projection<0) )
        {
            //calculate information about the hit
            var centerToEdge = Math.Sqrt(Math.Pow(Radius, 2)-Math.Pow(distance, 2));
            var hitDistance =(float)( projection - centerToEdge);
            var hitPosition = ray.Origin + hitDistance * ray.Direction;
            
            //apply shading to the spot
            var normal = GetNormal(hitPosition);
            return new RayHit(hitPosition, hitDistance,Color, this, normal);
        }
        return null;
    }

    /// <summary>
    /// returns the normal Vektor
    /// </summary>
    /// <param name="position">positon for witch the normal shall be calculated</param>
    /// <returns>the Normal Vektor</returns>

    public Vector3 GetNormal(Vector3 position)
    {
        var normal = position - Position;
        return Vector3.Normalize(normal);
    }
}