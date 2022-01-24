using System.Numerics;

namespace Engine_3D;

public abstract class SceneObject
{
    public Vector3 center;

    public SceneObject()
    {
        
    }
    public SceneObject(float x, float y, float z)
    {
        center = new Vector3(x, y, z);
    }

    public void Translate()
    {
        
    }

    public void Rotate()
    {
        
    }

    public void Scale()
    {
        
    }
}