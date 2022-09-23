namespace RankOfTank;

public interface IDataLoader
{
    Task<string> LoadDataAsync(Query query, User user, CancellationToken cancel);
}