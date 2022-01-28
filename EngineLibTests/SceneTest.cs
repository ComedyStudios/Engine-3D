using System;
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
        var cam = new Cam(0, 0, 10);
        
        var scene = new Scene();
        
        // act
        var result = scene.CamToWorldCordinate(640, 360, cam);

        // Validation
        Assert.AreEqual(0, Math.Round(result.X, 2));
        Assert.AreEqual(0, Math.Round(result.Y, 2));
    }
}