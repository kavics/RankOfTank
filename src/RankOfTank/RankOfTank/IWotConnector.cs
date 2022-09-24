namespace RankOfTank;

public interface IWotConnector
{
    Task<RoTData> DownloadUserDataAsync(User user, CancellationToken cancel);
}