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
            sceneObject.LocalAxis[0].X * localCoordinate.X + sceneObject.LocalAxis[1].X * localCoordinate.Y + sceneObject.LocalAxis[2].X * localCoordinate.Z + translation.X,
            sceneObject.LocalAxis[0].Y * localCoordinate.X + sceneObject.LocalAxis[1].Y * localCoordinate.Y + sceneObject.LocalAxis[2].Y * localCoordinate.Z + translation.Y,
            sceneObject.LocalAxis[0].Z * localCoordinate.X + sceneObject.LocalAxis[1].Z * localCoordinate.Y + sceneObject.LocalAxis[2].Z * localCoordinate.Z + translation.Z);
        return worldCoordinate;
    }
    
}