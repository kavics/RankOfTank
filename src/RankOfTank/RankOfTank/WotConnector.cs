using Microsoft.Extensions.Logging;

namespace RankOfTank;

public class WotConnector : IWotConnector
{
    private readonly IDataLoader _dataLoader;
    private readonly IDataStorage _dataStorage;
    private readonly ILogger _logger;


    public WotConnector(IDataLoader dataLoader, IDataStorage dataStorage, ILogger<WotConnector> logger)
    {
        _dataLoader = dataLoader;
        _dataStorage = dataStorage;
        _logger = logger;
    }

    public async Task<RoTData?> DownloadUserDataAsync(User user, CancellationToken cancel)
    {
        var storedData = await _dataStorage.LoadDataAsync(Query.AccountInfo, user, cancel).ConfigureAwait(false);
        if (storedData != null && storedData.CreationDate < DateTime.UtcNow.AddMinutes(10))
        {
            _logger.LogTrace("Data from storage");
            return storedData;
        }

        _logger.LogTrace("Data from web");
        var loadedData = await _dataLoader.LoadDataAsync(Query.AccountInfo, user, cancel);
        if(loadedData != null)
            await _dataStorage.SaveDataAsync(Query.AccountInfo, user, loadedData, cancel);

        return loadedData;
    }
}