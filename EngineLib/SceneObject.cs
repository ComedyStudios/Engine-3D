using System.Numerics;

namespace EngineLib;

public abstract class SceneObject
{
    public Vector3 center;
    public Vector3 XAxis = new Vector3(1, 0, 0);
    public Vector3 YAxis= new Vector3(0, 1, 0);
    public Vector3 ZAxis = new Vector3(0, 0, 1);

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