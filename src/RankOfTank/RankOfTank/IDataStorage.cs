namespace RankOfTank;

public interface IDataStorage : IDataLoader
{
    Task SaveDataAsync(Query query, User user, RoTData data, CancellationToken cancel);
}