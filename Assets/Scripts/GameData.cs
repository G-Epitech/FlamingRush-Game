using Unity.VisualScripting;

public class GameData
{
    
    public bool start { get; set; }
    public int lifes { get; set; }
    public int streak { get; set; }

    public GameData()
    {
        start = false;
        lifes = 3;
        streak = 0;
    }
}