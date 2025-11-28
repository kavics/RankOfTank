namespace RankOfTank;

public interface IWotConnector
{
    Task<RoTData?> DownloadUserDataAsync(User user, CancellationToken cancel);
    Task<RoTData?> DownloadUserGarageAsync(User user, CancellationToken cancel);
}