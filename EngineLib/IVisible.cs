using System.Drawing;
using System.Numerics;
using System.Windows.Media.Media3D;

namespace EngineLib;

public interface IVisible
{
    /// <summary>
    /// method that is supposed to process information if and the Ray did hit the Object with this Interface
    /// </summary>
    /// <param name="ray">the ray that was casted</param>
    /// <returns>information on what and where the ray hit</returns>
    public RayHit? RayCastHit(Ray ray, Scene scene);
}