using System.Numerics;

namespace EngineLib;

public class Ray
{
    /// <summary>
    /// constructor of the ray 
    /// </summary>
    /// <param name="origin">position the ray is being cast from</param>
    /// <param name="direction">direction of the ray</param>
    public Ray(Vector3 origin , Vector3 direction)
    {
        this.Origin = origin;
        this.Direction = direction;
    }
    /// <summary>
    /// the Origin of the Ray
    /// </summary>
    public Vector3 Origin { get; }
    /// <summary>
    /// direction of the Ray
    /// </summary>
    public Vector3 Direction { get; }

    /// <summary>
    /// checks what the ray hits 
    /// </summary>
    /// <param name="scene">scene in which the ray is casted/param>
    /// <returns>information on what the Ray hit, can return null</returns>
    public RayHit? RayCastHitAnyObject(Scene scene)
    {
        RayHit? hit = null;
        foreach (var thing in scene.Objects)
        {
            if (thing is IVisible visibleThing)
            {
                var newHit = visibleThing.RayCastHit(this);
                if (newHit != null && (hit == null || newHit.Distance < hit.Distance) )
                {
                    hit = newHit;
                }
            }
        }
        return hit;
    }
}

