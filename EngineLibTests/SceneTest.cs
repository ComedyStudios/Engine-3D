using EngineLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineLibTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void CamToWorldCordinateTest()
    {
        // initialisation
        var cam = new Cam(0, 0, 0);
        
        var scene = new Scene();
        
        // act
        var result = scene.CamToWorldCordinate(0, 0, cam);

        // Validation
        Assert.Equals(result.X, 10);
        Assert.Equals(result.Y, 10);
        Assert.Equals(result.Z, 10);
    }
}