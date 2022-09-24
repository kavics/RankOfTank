namespace RankOfTank;

public class DevNullDataStorage : IDataStorage
{
    public Task<RoTData> LoadDataAsync(Query query, User user, CancellationToken cancel)
    {
        return Task.FromResult((RoTData)null);
    }
    public Task SaveDataAsync(Query query, User user, string data, CancellationToken cancel)
    {
        return Task.CompletedTask;
    }
}

