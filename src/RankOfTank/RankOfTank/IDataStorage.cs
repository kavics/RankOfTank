namespace RankOfTank;

public interface IDataStorage : IDataLoader
{
    Task SaveDataAsync(Query query, User user, string data, CancellationToken cancel);
}