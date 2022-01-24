using System.Numerics;

namespace Engine_3D;

public class Cam: SceneObject
{
    public Vector3 direction { get; }
    private Vector3 orientation = new Vector3(0, 1, 0);
    
    
    public Cam(float x, float y, float z, Vector3 direction)
    {
        center = new Vector3(x, y, z);
        this.direction = direction;
    }
}