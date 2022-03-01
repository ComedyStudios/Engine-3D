using System.Numerics;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Vector = System.Windows.Vector;

namespace EngineLib;

public abstract class SceneObject
{
    protected SceneObject(float x, float y, float z, float albedo, float reflectivity, Color color)
    {
        Position = new Vector3(x, y, z);
        Albedo = albedo;
        Reflectivity = reflectivity;
        Color = color;
    }

    protected SceneObject(float x, float y, float z, Color color)
    {
        Position = new Vector3(x, y, z);
        Color = color;
    }

    protected SceneObject(float x, float y, float z)
    {
        Position = new Vector3(x, y, z);
    }
    
    /// <summary>
    /// position and Axis of the Object
    /// </summary>
    public Vector3 Position;

    private static Vector3 XAxis = new(1, 0, 0);
    private static Vector3 YAxis= new(0, 1, 0);
    private static Vector3 ZAxis = new(0, 0, 1);

    public List<Vector3> LocalAxis = new List<Vector3>() {XAxis, YAxis, ZAxis};


    /// <summary>
    /// Reflectiveness of the Sphere
    /// </summary>
    public float Albedo { get; set; }
    public float Reflectivity { get; set;}
    
    /// <summary>
    /// Color of the Object
    /// </summary>
    public Color Color { get; set; }

    public void MoveObject(Vector3 translationVector)
    {
        Position += translationVector;
    }

    public void RotateOnGlobal(float angleX, float angleY, float angleZ)
    {
        angleX = (float)((Math.PI / 180) * angleX);
        angleY = (float)((Math.PI / 180) * angleY);
        angleZ = (float)((Math.PI / 180) * angleZ);

        LocalAxis[0] = RotateVectorOnX(angleX, new(1, 0, 0));
        LocalAxis[0] = RotateVectorOnY(angleY, LocalAxis[0]);
        LocalAxis[0] = RotateVectorOnZ(angleZ, LocalAxis[0]);
        
        LocalAxis[1] = RotateVectorOnX(angleX, new(0, 1, 0));
        LocalAxis[1] = RotateVectorOnY(angleY, LocalAxis[1]);
        LocalAxis[1] = RotateVectorOnZ(angleZ, LocalAxis[1]);
        
        LocalAxis[2] = RotateVectorOnX(angleX, new(0, 0, 1));
        LocalAxis[2] = RotateVectorOnY(angleY, LocalAxis[2]);
        LocalAxis[2] = RotateVectorOnZ(angleZ, LocalAxis[2]);
    }
    
    public void RotateOnLocal(float angleX, float angleY, float angleZ)
    {
        angleX = (float)((Math.PI / 180) * angleX);
        angleY = (float)((Math.PI / 180) * angleY);
        angleZ = (float)((Math.PI / 180) * angleZ);

        for (int i = 0; i < LocalAxis.Count; i++)
        {
            LocalAxis[i] = RotateVectorOnX(angleX, LocalAxis[i]);
            LocalAxis[i] = RotateVectorOnY(angleY, LocalAxis[i]);
            LocalAxis[i] = RotateVectorOnZ(angleZ, LocalAxis[i]);
        }
    }
    
    //TODO: Fix some computation error that is probably here
    private Vector3 RotateVectorOnX(float angle, Vector3 vector3)
    {
        vector3= new Vector3(vector3.X, (float)(Math.Cos(angle) * vector3.Y - Math.Sin(angle) * vector3.Z), (float)(Math.Sin(angle) * vector3.Y + Math.Cos(angle) * vector3.Z));
        return vector3;
    }
    private Vector3 RotateVectorOnY(float angle, Vector3 vector3)
    {
        vector3= new Vector3((float)(Math.Cos(angle) * vector3.X+Math.Sin(angle)*vector3.Z), vector3.Y, (float)(-Math.Sin(angle) * vector3.X+ Math.Cos(angle) * vector3.Z));
        return vector3;
    }
    private Vector3 RotateVectorOnZ(float angle, Vector3 vector3)
    {
        vector3= new Vector3((float)(Math.Cos(angle)* vector3.X- Math.Sin(angle) * vector3.Y), (float)(Math.Sin(angle)* vector3.X + Math.Cos(angle) * vector3.Y), vector3.Z);
        return vector3;
    }
}