using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RankOfTank.WotModels;

namespace RankOfTank;

public class RoTController : IRoTController
{
    private IUserStore _userStore;
    private IWotConnector _connector;

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