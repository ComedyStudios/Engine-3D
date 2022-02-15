using System;
using EngineLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EngineLibTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void CamToWorldCoordinateTest()
    {
        // initialisation
        var cam = new Camera(0, 0, 10,90, 1280, 720);
        
        var scene = new Scene();
        
        // act
        var result = cam.CameraToWorldCoordinate(640, 360);

        // Validation
        Assert.AreEqual(0, Math.Round(result.X, 2));
        Assert.AreEqual(0, Math.Round(result.Y, 2));
    }
}