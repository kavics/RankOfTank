namespace RankOfTank;

public class InMemoryDataStorage : IDataStorage
{
    private readonly Dictionary<string, List<RoTData>> _storage = new();

    public Task<RoTData?> LoadDataAsync(Query query, User user, CancellationToken cancel)
    {
        _storage.TryGetValue(GetKey(query, user), out var existingData);
        return Task.FromResult(existingData?.LastOrDefault());
    }

    public Task SaveDataAsync(Query query, User user, RoTData data, CancellationToken cancel)
    {
        var key = GetKey(query, user);
        if (!_storage.TryGetValue(key, out var existingList))
        {
            existingList = new List<RoTData>();
            _storage[key] = existingList;
        }

        if (existingList.All(x => x.CreationDate != data.CreationDate))
            existingList.Add(data);

        return Task.CompletedTask;
    }

    private string GetKey(Query query, User user)
    {
        return $"{user.AccountId}-{query}";
    }
}

