using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RankOfTank.WotModels;

namespace RankOfTank;

public class RoTController : IRoTController
{
    private readonly IUserStore _userStore;
    private readonly IWotConnector _connector;

    public RoTController(IUserStore userStore, IWotConnector connector)
    {
        _userStore = userStore;
        _connector = connector;
    }

    public async Task<WotUserData?> GetUserDataAsync(string userName, CancellationToken cancel)
    {
        var user = _userStore.GetUser(userName);
        if (user == null)
            return null;

        var rotData = await _connector.DownloadUserDataAsync(user, cancel).ConfigureAwait(false);
        if (rotData == null)
            return null;

        var userData = DeserializeData<WotUserData>(rotData.Data, user);
        return userData;
    }

    public async Task<WotGarageData?> GetUserGarageAsync(string userName, CancellationToken cancel)
    {
        var user = _userStore.GetUser(userName);
        if (user == null)
            return null;

        var rotData = await _connector.DownloadUserGarageAsync(user, cancel).ConfigureAwait(false);
        if (rotData == null)
            return null;

        var garageData = DeserializeData<WotGarageDataResponseItem[]>(rotData.Data, user);
        if (garageData == null)
            return null;

        return new WotGarageData {TankIds = garageData.Select(x => x.TankId).ToArray()};
    }

    private T? DeserializeData<T>(string source, User user) where T : class
    {
        var jObject = JsonConvert.DeserializeObject(source) as JObject;

        var src = jObject?["data"]?[user.AccountId]?.ToString();
        if (src == null)
            return null;

        var result = JsonConvert.DeserializeObject<T>(src);

        return result;
    }
}