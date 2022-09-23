namespace RankOfTank;

public interface IUserStore
{
    void AddUser(User user);
    User? GetUser(string userName);
    string[] GetUserNames();
}