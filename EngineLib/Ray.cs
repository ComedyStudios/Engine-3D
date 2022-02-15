using System.Drawing;
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
        Origin = origin;
        Direction = direction;
    }
    /// <summary>
    /// the Origin of the Ray
    /// </summary>
    public Vector3 Origin { get; }

    /// <summary>
    /// Direction of the Vector
    /// </summary>
    public Vector3 Direction { get; }

    /// <summary>
    /// checks what the ray hits 
    /// </summary>
    /// <param name="scene">scene in which the ray is casted/param>
    /// <returns>information on what the Ray hit, can return null</returns>
    public RayHit? RayCastAndShade(Scene scene, int raytracingDepth)
    {
        //check if ray Hit Anything
        var hit = RayCastHit(scene);
        //Apply DiffuseShading
        if (hit != null)
        {
            hit.PixelColor = Reflection(raytracingDepth, hit, this, scene);
        }
        return hit;
    }
    
    /// <summary>
    /// checks if a certain Object is visible for a certain ray
    /// </summary>
    /// <param name="scene"></param>
    /// <returns>hit informaiton</returns>
    private RayHit? RayCastHit(Scene scene)
    {
        RayHit? hit = null;
        foreach (var thing in scene.Objects)
        {
            if (thing is IVisible visibleThing)
            {
                var newHit = visibleThing.RayCastHit(this, scene);
                if (newHit != null && (hit == null || newHit.Distance < hit.Distance))
                {
                    hit = newHit;
                }
            }
        }
        return hit;
    }
    
    /// <summary>
    /// applies shading to a certain pixel
    /// </summary>
    /// <param name="hit">hit information</param>
    /// <param name="scene">Scene</param>
    /// <returns>new Color with the DiffuseShading applied</returns>
    private Color DiffuseShading(RayHit hit, Scene scene)
    {
        if (scene == null) throw new ArgumentNullException(nameof(scene));
        
        var finalColor = Color.Black;
        
        foreach (var lightSource in scene.Lightsources)
        {
            //calculate values
            var lightDir = Vector3.Normalize(lightSource.Position - hit.HitLocation);
            var lightPower = Math.Max(Vector3.Dot(hit.Normal, lightDir),0 ) * lightSource.Intensity/(4*Math.PI * lightDir.Length());
            var lightFactor = lightPower * hit.SceneObject.Albedo / Math.PI;;
            
            //change colors
            if (SpotInShadow(hit, lightDir, scene)) continue;
            finalColor = finalColor.AddColor(hit.PixelColor.ColorMultiply(lightFactor).ColorMultiply(lightSource.Color));
        }
        return finalColor;
    }
    /// <summary>
    /// checks if a spot is in shadow
    /// </summary>
    /// <param name="hit">the spot that is being checked</param>
    /// <param name="lightDirection">Direction to a certain light from this posiiton</param>
    /// <param name="scene">scene that all the things are placed in</param>
    /// <returns>bool</returns>
    private static bool SpotInShadow(RayHit hit, Vector3 lightDirection, Scene scene)
    {
        float bias = 0.0001f;
        var ray = new Ray(hit.HitLocation + bias * hit.Normal, lightDirection);
        var shadowRayHit = ray.RayCastHit(scene);
        
        if (shadowRayHit == null || shadowRayHit.SceneObject == hit.SceneObject)
        {
            return false;
        }
        return true;
    }
    private Color Reflection( int maxIteration, RayHit hit, Ray incedentRay, Scene scene, int iteration = 0)
    {
        var color = DiffuseShading(hit, scene).ColorMultiply(1-hit.SceneObject.Reflectivity);
        if (iteration < maxIteration)
        {
            if (hit.SceneObject.Reflectivity != 0)
            {
                var bias = 0.001f;
                var reflectedRayDir = incedentRay.Direction - 2 * Vector3.Dot(incedentRay.Direction, hit.Normal) * hit.Normal;
                var reflectionRay = new Ray(hit.HitLocation + bias * hit.Normal ,reflectedRayDir);
                var newHit = reflectionRay.RayCastHit(scene);
                if (newHit != null)
                {
                    color = color.AddColor(Reflection(maxIteration, newHit, reflectionRay, scene, iteration + 1).ColorMultiply(hit.SceneObject.Reflectivity));
                }
            }
        }
        return color;
    }
}

