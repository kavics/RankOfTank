namespace RankOfTank;

public interface IWotConnector
{
    Task<string> DownloadUserDataAsync(User user, CancellationToken cancel);
}