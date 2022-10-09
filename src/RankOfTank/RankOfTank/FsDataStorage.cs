using System;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace RankOfTank;

public class FsDataStorageOptions
{
    public string DatabaseDirectory { get; set; }
}

public class FsDataStorage : IDataStorage
{
    private readonly FsDataStorageOptions _options;

    public FsDataStorage(IOptions<FsDataStorageOptions> options)
    {
        _options = options?.Value ?? throw new ArgumentException("Missing FsDataStorageOptions.");
    }

    public Task<RoTData?> LoadDataAsync(Query query, User user, CancellationToken cancel)
    {
        var directoryName = GetDirectoryName(query, user);
        if (!Directory.Exists(directoryName))
            return Task.FromResult((RoTData?)null);

        var fileName = Directory.GetFiles(directoryName).MaxBy(x => x);
        if(fileName == null)
            return Task.FromResult((RoTData?)null);

        var serializer = new JsonSerializer();
        using var streamReader = new StreamReader(fileName);
        using var reader = new JsonTextReader(streamReader);
        var rotData = serializer.Deserialize<RoTData>(reader);

        return Task.FromResult(rotData);
    }

    public Task SaveDataAsync(Query query, User user, RoTData data, CancellationToken cancel)
    {
        var fileName = GetFileName(query, user, data.CreationDate, out var directory);
        if (File.Exists(fileName))
            return Task.CompletedTask;
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        using var writer = new StreamWriter(fileName, Encoding.UTF8,
            new FileStreamOptions {Access = FileAccess.Write, Mode = FileMode.CreateNew});
        var serializer = new JsonSerializer();
        serializer.Serialize(writer, data);
        return Task.CompletedTask;
    }

    private string GetDirectoryName(Query query, User user)
    {
        var directoryName = Path.Combine(
            _options.DatabaseDirectory,
            user.AccountId,
            query.ToString());
        return directoryName;
    }

    private string GetFileName(Query query, User user, DateTime dateTime, out string directoryName)
    {
        directoryName = GetDirectoryName(query, user);
        var path = Path.Combine(
            directoryName,
            $"{dateTime:yyyy-MM-dd HH-mm-ss}.json");
        return path;
    }
}
