using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace Engine_3D;

public class Sphere : SceneObject, IVisible
{
    private float radius;
    public Color _color = Color.Green;
    public Sphere(float x, float y,float z, float radius)
    {
        center = new Vector3(x, y, z);
        this.radius = radius;
    }


    public bool RayCastHit(Ray ray)
    {
        /*for (int i = 0;i < 100; i++)
        {
            var temp1 = ray.origin + i * ray.direction;
            Console.WriteLine(temp1);
        }*/
        
        var transform = center - ray.origin;
        var projection = Vector3.Dot(transform, ray.direction);
        var temp = Vector3.Dot(transform, transform);
        var distance = Math.Sqrt(temp- projection * projection);
        if (distance < radius && !(projection<0) )
        {
            return true;
        }

        return false;
    }
}