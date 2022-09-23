namespace RankOfTank;

public class WotConnector : IWotConnector
{
    private readonly IDataLoader _dataLoader;

    public WotConnector(IDataLoader dataLoader)
    {
        _dataLoader = dataLoader;
    }

    public Task<string> DownloadUserDataAsync(User user, CancellationToken cancel)
    {
        return _dataLoader.LoadDataAsync(Query.AccountInfo, user, cancel);
    }
}