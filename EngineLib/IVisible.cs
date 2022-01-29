namespace EngineLib;

public interface IVisible
{
    public RayHit? RayCastHit(Ray ray);
}