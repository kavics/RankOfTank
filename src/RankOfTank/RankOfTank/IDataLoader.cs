namespace RankOfTank;

public interface IDataLoader
{
    /// <summary>
    /// Loads last available data by given <see cref="Query"/>.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="user"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<RoTData> LoadDataAsync(Query query, User user, CancellationToken cancel);
}