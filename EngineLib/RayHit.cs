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
    public RayHit(Vector3 hitLocation, float hitDistance, Color color, SceneObject sceneObject, Vector3 normal)
    {
        HitLocation = hitLocation;
        Distance = hitDistance;
        PixelColor = color;
        SceneObject = sceneObject;
        Normal = normal;
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
    
    /// <summary>
    /// Normal Vector at hit location
    /// </summary>
    public Vector3 Normal { get; set; }    

    public SceneObject SceneObject { get; set; }
    /// <summary>
    /// the location the ray has hit at
    /// </summary>
    public Vector3 HitLocation { get; set; }
    
    /// <summary>
    /// the color of the Pixel
    /// </summary>
    public Color PixelColor { get; set; }
}