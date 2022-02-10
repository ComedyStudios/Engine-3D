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
        this.Origin = origin;
        this.Direction = direction;
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
    public RayHit? RayCastAndShade(Scene scene)
    {
        //check if ray Hit Anything
        RayHit? hit; 
        hit = RayCastHit(scene);
        
        //Apply Shading
        if (hit != null)
        {
            hit.PixelColor = Shading(hit, scene);
        }
       
        return hit;
    }

    public RayHit? RayCastHit(Scene scene)
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
    /// <returns>new Color with the Shading applied</returns>
    public Color Shading(RayHit hit, Scene scene)
        {
            if (scene == null) throw new ArgumentNullException(nameof(scene));
            var finalColor = Color.Black;
            
            foreach (var lightSource in scene.Lightsources)
            {
                //calculate values
                var lightDir = Vector3.Normalize(lightSource.Position - hit.HitLocation);
                var lightDot = Math.Max(Vector3.Dot(lightDir,hit.Normal), 0);
                var lightReflected = hit.SceneObject.Albedo / Math.PI;
                var lightPower = lightDot * lightSource.Intensity;
                //TODO: check for shadows
                
                 if (!SpotInShadow(hit, lightDir, scene))
                 { 
                     var newColor = calculateColorValue(hit.PixelColor, lightPower, lightReflected);
                     finalColor = AddColors(finalColor, newColor);
                 }
            }
            return finalColor;
        }

        private Color calculateColorValue(Color colorValue, float lightPower, double lightReflected)
        {
            var r = ((float)colorValue.R / 255) * lightPower * lightReflected;
            var g = ((float)colorValue.G / 255) * lightPower * lightReflected;
            var b = ((float)colorValue.B / 255) * lightPower * lightReflected;
            return Color.FromArgb(Math.Min((int)(r * 255), 255), Math.Min((int)(g * 255), 255), Math.Min((int)(b * 255), 255));
        }


        private static Color AddColors(Color color1, Color color2)
        {
            return Color.FromArgb(Math.Min(255, color1.R + color2.R), Math.Min(255, color1.G + color2.G), Math.Min(255, color1.B + color2.B));
        }

        private bool SpotInShadow(RayHit hit, Vector3 lightDirection, Scene scene)
        {
             
            var ray = new Ray(hit.HitLocation, lightDirection);
            var shadowRayHit = ray.RayCastHit(scene);
            //TODO: Add bias so that it casts correctly
            
            if (shadowRayHit == null || ((shadowRayHit.HitLocation - hit.HitLocation).Length() < 0.1 && shadowRayHit.SceneObject != hit.SceneObject))
            {
                return false;
            }
            return true;
        }
}

