using System.Drawing;
using System.Numerics;

namespace EngineLib;

public class Lightsource: SceneObject
{
    private float _intenisty;
    public Lightsource(float x, float y, float z, float intensity)
    {
        Position = new Vector3(x, y, z);
        Intensity = intensity;
    }

    public float Intensity
    {
        get => _intenisty;
        set
        {
            if (value >= 0)
            {
                _intenisty = value;
            }
        }
    }
}