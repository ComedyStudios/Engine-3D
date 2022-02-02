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
    


    public RayHit? RayCastHit(Ray ray, Scene scene)
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
            
            //TODO: doesnt support multiple lightsources
            var newColor = Shading(hitPosition, scene.Lightsources[0]);
            return new RayHit(hitPosition, hitDistance,newColor, this);
        }
        return null;
    }

    private Color Shading(Vector3 hitPosition, Lightsource lightsource)
    {
        var lightDir = Vector3.Normalize(lightsource.Position - Position);
        var lightFaktor = Math.Max(Vector3.Dot(lightDir,GetNormal(hitPosition)), 0);
        var color = Color.FromArgb((int)(_color.R * lightFaktor), (int)(_color.G * lightFaktor), (int)(_color.B * lightFaktor));
        return color;
    }


    public Vector3 GetNormal(Vector3 hitPosition)
    {
        var normal = hitPosition - Position;
        return Vector3.Normalize(normal);
    }
}