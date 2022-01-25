using System.Numerics;

namespace Engine_3D;

public class Ray
{
    //the pixels the ray is being cast from
    public Vector3 origin { get; }
    public Vector3 direction { get; }

    public Ray(Vector3 origin , Vector3 direction)
    {
        this.origin = origin;
        this.direction = direction;
    }

    public bool RayCastHit(Scene scene)
    {
        foreach (var thing in scene.Objects)
        {
            if (thing is IVisible)
            {
                IVisible visibleThing = (IVisible)thing;
                if (visibleThing.RayCastHit(this))
                {
                    return true;
                }
            }
        }
        return false;
    }
}