public class GameData
{
    public uint lifes { get; set; }
    public uint streak { get; set; }

    public GameData()
    {
        lifes = 3;
        streak = 0;
    }
}