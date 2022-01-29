using System.Numerics;

namespace EngineLib;

public static class Utils
{
    public static Vector3 LocalToGlobalCoordinate(Vector3 localCoordinate, SceneObject sceneObject, Vector3 worldOrigin)
    {
        var translation = sceneObject.Position - worldOrigin;
        var worldCoordinate = new Vector3(
            sceneObject.XAxis.X * localCoordinate.X + sceneObject.YAxis.X * localCoordinate.Y + sceneObject.ZAxis.X * localCoordinate.Z + translation.X,
            sceneObject.XAxis.Y * localCoordinate.X + sceneObject.YAxis.Y * localCoordinate.Y + sceneObject.ZAxis.Y * localCoordinate.Z + translation.Y,
            sceneObject.XAxis.Z * localCoordinate.X + sceneObject.YAxis.Z * localCoordinate.Y + sceneObject.ZAxis.Z * localCoordinate.Z + translation.Z);
        return worldCoordinate;
    }
}