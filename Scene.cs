using System.Collections.Generic;
using System.Numerics;
using System.Windows.Documents;

namespace Engine_3D;

public class Scene
{
    public Cam mainCam;
    private Sphere s1;
    public List<SceneObject> Objects = new List<SceneObject>();

    public Scene()
    {
        mainCam = new Cam(-20, 0, 0, new Vector3(1,0, 0));
        s1  = new Sphere(0, 0,0, 10);
        
    }
}