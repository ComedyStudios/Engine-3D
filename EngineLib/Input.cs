using System.Windows;

namespace EngineLib;

public class Input
{
    public Input()
    {
        MouseDeltaX = 0;
        MouseDeltaY = 0;
        HorizontalMovement = 0;
        VerticalMovement = 0;
        DeltaTime = 0;
        MovementSpeed = 0;
    }
    public float HorizontalMovement { get; set; }

    public float VerticalMovement { get; set;}
    
    public double DeltaTime { get; set; }
    public float MovementSpeed { get; set; }
    
    public double MouseDeltaX { get; set; }
    public double MouseDeltaY { get; set; }
}