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
        throw new System.NotImplementedException();
    }
}