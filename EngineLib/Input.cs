namespace EngineLib;

public class Input
{
    public Input()
    {
        HorizontalMovement = 0;
        VerticalMovement = 0;
        DeltaTime = 0;
        MovementSpeed = 0;
    }
    public float HorizontalMovement { get; set; }

    public float VerticalMovement { get; set;}
    
    public float DeltaTime { get; set; }
    public float MovementSpeed { get; set; }
}