using System.Numerics;

namespace EngineLib;

public class Ray
{
    public Ray(Vector3 origin , Vector3 direction)
    {
        this.Origin = origin;
        this.Direction = direction;
    }
    
    //the pixels the ray is being cast from
    public Vector3 Origin { get; }
    public Vector3 Direction { get; }

    public RayHit? RayCastHit(Scene scene)
    {
        foreach (var thing in scene.Objects)
        {
            if (thing is IVisible visibleThing)
            {
                return visibleThing.RayCastHit(this);
            }
        }
        return null;
    }
}