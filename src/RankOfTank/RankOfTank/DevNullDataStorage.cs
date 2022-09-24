namespace RankOfTank;

public class DevNullDataStorage : IDataStorage
{
    public Task<RoTData?> LoadDataAsync(Query query, User user, CancellationToken cancel)
    {
        return Task.FromResult(default(RoTData));
    }
    public Task SaveDataAsync(Query query, User user, RoTData data, CancellationToken cancel)
    {
        return Task.CompletedTask;
    }
}

