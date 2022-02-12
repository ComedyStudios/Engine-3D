using System.Numerics;

namespace EngineLib;

public static class Vector3Extensions
{
    /// <summary>
    /// converts the Local Vektor to Global Vektor 
    /// </summary>
    /// <param name="localCoordinate">the local position</param>
    /// <param name="sceneObject">the Object on which the conversion takes place</param>
    /// <param name="worldOrigin"></param>
    /// <returns>WorldCoordinate</returns>
    public static Vector3 LocalToGlobalCoordinate(this Vector3 localCoordinate, SceneObject sceneObject)
    {
        var translation = sceneObject.Position;
        var worldCoordinate = new Vector3(
            sceneObject.XAxis.X * localCoordinate.X + sceneObject.YAxis.X * localCoordinate.Y + sceneObject.ZAxis.X * localCoordinate.Z + translation.X,
            sceneObject.XAxis.Y * localCoordinate.X + sceneObject.YAxis.Y * localCoordinate.Y + sceneObject.ZAxis.Y * localCoordinate.Z + translation.Y,
            sceneObject.XAxis.Z * localCoordinate.X + sceneObject.YAxis.Z * localCoordinate.Y + sceneObject.ZAxis.Z * localCoordinate.Z + translation.Z);
        return worldCoordinate;
    }
    
}