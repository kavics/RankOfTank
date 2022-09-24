namespace RankOfTank;

public class UserData
{
    public string? Name { get; set; }
    public string? AccountId { get; set; }
}

public class UserOptions
{
    public IEnumerable<UserData>? Users { get; set; }
}