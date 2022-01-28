using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Windows.Media.Media3D;

namespace EngineLib;

public class Sphere : SceneObject, IVisible
{
    private float radius;
    public Color _color = Color.Green;
    public Sphere(float x, float y,float z, float radius)
    {
        Position = new Vector3(x, y, z);
        this.radius = radius;
    }


    public bool RayCastHit(Ray ray)
    {
        var transform = Position - ray.Origin;
        var projection = Vector3.Dot(transform, ray.Direction);
        var temp = Vector3.Dot(transform, transform);
        var distance = Math.Sqrt(temp- projection * projection);
        if (distance < radius && !(projection<0) )
        {
            return true;
        }
        return false;
    }
}