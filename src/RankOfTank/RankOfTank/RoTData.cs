namespace RankOfTank;

public class RoTData
{
    public DateTime CreationDate { get; set; }
    public string Data { get; }

    public RoTData(string data)
    {
        Data = data;
        CreationDate = DateTime.UtcNow;
    }
}
