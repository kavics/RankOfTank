using Microsoft.Extensions.Options;

namespace RankOfTank;

public class UserStore : IUserStore
{
    private readonly Dictionary<string, User> _users = new ();

    public UserStore(IOptions<UserOptions> configuration)
    {
        var users = configuration?.Value.Users;
        if(users != null)
            foreach (var userData in users)
                AddUser(new User{Name = userData.Name, AccountId = userData.AccountId});
    }

    public virtual void AddUser(User user) => _users[user.Name] = user;

    public virtual User? GetUser(string userName) => _users.TryGetValue(userName, out var existing) ? existing : null;

    public virtual string[] GetUserNames() => _users.Keys.ToArray();
}