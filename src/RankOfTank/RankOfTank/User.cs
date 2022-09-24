namespace RankOfTank;

public class User
{
    public string Name { get; }
    public string AccountId { get; }

    public User(string name, string accountId)
    {
        Name = name;
        AccountId = accountId;
    }
}