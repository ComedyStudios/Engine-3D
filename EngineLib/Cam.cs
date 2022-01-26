using System.Numerics;

namespace EngineLib;

public class Cam: SceneObject
{
    public int WindowWidth = 1280;
    public int WindowHeight = 720;
    public float fov = 90;
    
    
    public Cam(float x, float y, float z)
    {
        center = new Vector3(x, y, z);
    }

    
}