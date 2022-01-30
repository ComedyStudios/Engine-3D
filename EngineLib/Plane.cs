using System.Drawing;
using System.Numerics;

namespace EngineLib;

public class Plane: SceneObject, IVisible
{
    //TODO: Make the plane not infinite size
    /// <summary>
    /// constructor of the plane
    /// </summary>
    /// <param name="x">Coordinate of the Object</param>
    /// <param name="y">Coordinate of the Object</param>
    /// <param name="z">Coordinate of the Object</param>
    /// <param name="width">width of the plane</param>
    /// <param name="height">height of the plane</param>
    /// <param name="color">color of the plane</param>
    public Plane(float x, float y, float z,float width, float height, Color color)
    {
        Position = new Vector3(x, y, z);
        Width = width;
        Height = height;
        Color = color;
    }

    /// <summary>
    /// return the Normal Vector of the plane which determines its orientation. The Normal Vector is facing the same way as the Y vector
    /// </summary>
    public Vector3 NormalVector => YAxis;
    
    /// <summary>
    /// width of the plane
    /// </summary>
    public float Width { get; set; }
    /// <summary>
    /// Height of the plane
    /// </summary>
    public float Height { get; set; }
    
    /// <summary>
    /// Color of the plane
    /// </summary>
    public Color Color { get; set; }
    
    /// <summary>
    /// checks if the plane is visible at a certain pixel or not
    /// </summary>
    /// <param name="ray">the ray the is being checked</param>
    /// <returns>hit information</returns>
    public RayHit? RayCastHit(Ray ray)
    {
        var distance = (Vector3.Dot((Position - ray.Origin), NormalVector)) / Vector3.Dot(ray.Direction, NormalVector);
        if (distance >= 0)
        {
            var hitLocation = ray.Origin + distance * ray.Direction;
            var centerIntersectionVector = hitLocation - Position;
            var proj1 = Vector3.Dot(centerIntersectionVector, new Vector3(Width, 0, 0)) / Width;
            var proj2 = Vector3.Dot(centerIntersectionVector, new Vector3(0, 0, Height)) / Height;


            if ((proj1 < Width && proj1 > 0)&&(proj2 < Height && proj2 > 0))
            {
                var hit = new RayHit(hitLocation, distance, Color);
                return hit;
            }
        }
        return null;
    }
}