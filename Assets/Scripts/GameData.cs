using Unity.VisualScripting;

public class GameData
{
    
    public bool start { get; set; }
    public uint lifes { get; set; }
    public uint streak { get; set; }

    public GameData()
    {
        start = false;
        lifes = 3;
        streak = 0;
    }
}