using System.Drawing;
using System.Numerics;

namespace EngineLib;

public class RayHit
{
    private float _originToHitDistance;
    public RayHit(Vector3 hitLocation, float hitDistance, Color color)
    {
        HitLocation = hitLocation;
        Distance = hitDistance;
        PixelColor = color;
    }

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

    public Vector3 HitLocation { get; set; }
    public Color PixelColor { get; set; }
}