using System.Drawing;
using System.Numerics;

namespace EngineLib;

public class RayHit
{
    private float _originToHitDistance;
    /// <summary>
    /// constructor of the Ray hit
    /// </summary>
    /// <param name="hitLocation">location where the ray hit</param>
    /// <param name="hitDistance">distance to Ray origin</param>
    /// <param name="color">color of the Pixel</param>
    public RayHit(Vector3 hitLocation, float hitDistance, Color color, SceneObject sceneObject)
    {
        HitLocation = hitLocation;
        Distance = hitDistance;
        PixelColor = color;
        SceneObject = sceneObject;
    }
    
    /// <summary>
    /// returns the distance between hit and origin position 
    /// </summary>
    public float Distance
    {
        get =>_originToHitDistance ;
        set
        {
            if (value >= 0)
            {
                _originToHitDistance = value;
            }
        }
    }
    
    public SceneObject SceneObject { get; set; }
    /// <summary>
    /// the location the ray has hit at
    /// </summary>
    public Vector3 HitLocation { get; set; }
    
    /// <summary>
    /// the color of the Pixel
    /// </summary>
    public Color PixelColor { get; set; }
    
    public bool SpotInShow( Scene scene)
    {
        foreach (var lightsource in scene.Lightsources)
        {
            var ray = new Ray(lightsource.Position, Vector3.Normalize(this.HitLocation-lightsource.Position));
            var newHit = ray.RayCastHitAnyObject(scene);
            if (newHit != null && (this.HitLocation - newHit.HitLocation).Length()<0.001 )
            {
                return false;
            }
            
            //TODO: Fix this
        }
        return true;
    }
}