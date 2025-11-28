using RankOfTank.WotModels;

namespace RankOfTank;

public interface IRoTController
{
    Task<WotUserData?> GetUserDataAsync(string userName, CancellationToken cancel);
    Task<WotGarageData?> GetUserGarageAsync(string userName, CancellationToken cancel);
}