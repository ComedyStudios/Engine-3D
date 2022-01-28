using System.Numerics;
using System.Windows.Media.Media3D;
using EngineLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineLibTests;

[TestClass]

public class HitScanTest
{
    [TestMethod]
    public void SphereColisionTest()
    {
        var cam = new Cam(0,0,-10);
        var scene = new Scene();
        var s1 = new Sphere(0, 0, 100,1);
        var ray = new Ray(cam.Position, scene.CamToWorldCordinate(640, 360, cam)); ;
        var result = s1.RayCastHit(ray);
        Assert.IsTrue(result);
    }
}