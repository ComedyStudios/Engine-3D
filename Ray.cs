using System.Numerics;

namespace Engine_3D;

public class Ray
{
    //the pixels the ray is being cast from
    private Vector3 origin;
    private Vector3 direction;
    
    public Ray(Vector3 origin , Vector3 direction)
    {
        this.origin = origin;
        this.direction = direction;
    }

    public bool RayCastHit(Scene scene)
    {
        var rayHit = false;
        foreach (var thing in scene.Objects)
        {
            if (thing is IVisible)
            {
                IVisible visibleThing = (IVisible)thing;
                visibleThing.RayCastHit(this);
            }
        }
        return rayHit;
    }
}