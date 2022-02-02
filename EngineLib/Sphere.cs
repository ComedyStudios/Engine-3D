using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Windows.Media.Media3D;

namespace EngineLib;

public class Sphere : SceneObject, IVisible
{
    private float _radius;
    private Color _color;
    public Sphere(float x, float y,float z, float radius, Color color)
    {
        Position = new Vector3(x, y, z);
        this._radius = radius;
        _color = color;
    }


    public RayHit? RayCastHit(Ray ray)
    {
        var transform = Position - ray.Origin;
        var projection = Vector3.Dot(transform, ray.Direction);
        var temp = Vector3.Dot(transform, transform);
        var distance = Math.Sqrt(temp- projection * projection);
        if (distance < _radius && !(projection<0) )
        {
            var centerToEdge = Math.Sqrt(Math.Pow(_radius, 2)-Math.Pow(distance, 2));
            var hitDistance =(float)( projection - centerToEdge);
            var hitPosition = ray.Origin + hitDistance * ray.Direction;
            return new RayHit(hitPosition, hitDistance,_color, this);
        }
        return null;
    }
}