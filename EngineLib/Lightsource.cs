using System.Drawing;
using System.Numerics;

namespace EngineLib;

public class Lightsource: SceneObject
{
    private float _intenisty;

    /// <summary>
    /// construcktor of the Class
    /// </summary>
    /// <param name="x">x coordinate</param>
    /// <param name="y">y coordinate</param>
    /// <param name="z">z coordinate</param>
    /// <param name="intensity">light intesity</param>
    /// <param name="color">color of the light</param>
    public Lightsource(float x, float y, float z, float intensity, Color color): base(x, y, z, color)
    {
        Intensity = intensity;
    }
    
    
    /// <summary>
    /// Intensity of the Lightsource
    /// </summary>
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