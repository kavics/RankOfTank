using System.Net;
using Microsoft.Extensions.Options;

namespace RankOfTank;

internal class WebLoader : IDataLoader
{
    // See https://developers.wargaming.net/reference

    public static readonly string Host = "api.worldoftanks.eu";

    private readonly AccessOptions _accessOptions;

    public WebLoader(IOptions<AccessOptions> accessOptions)
    {
        _accessOptions = accessOptions.Value;
    }

    public async Task<RoTData?> LoadDataAsync(Query query, User user, CancellationToken cancel)
    {
        var url = GetUrl(query, user);
        var data = await ProcessWebRequestResponseAsync(url, HttpMethod.Get, cancel).ConfigureAwait(false);
        return new RoTData(data) {CreationDate = DateTime.UtcNow};
    }

    private string GetUrl(Query query, User user)
    {
        switch (query)
        {
            case Query.AccountInfo:
                return $"http://{Host}/wot/account/info/" +
                          $"?application_id={_accessOptions.Access?.ApiKey}" +
                          $"&account_id={user.AccountId}";

            default:
                throw new ArgumentOutOfRangeException(nameof(query), query, null);
        }
    }

    private async Task<string> ProcessWebRequestResponseAsync(string url, HttpMethod method,
        CancellationToken cancellationToken)
    {
        using var handler = new HttpClientHandler();
        using var client = new HttpClient(handler);
        using var request = new HttpRequestMessage(method, url);

        // this will close the connection instead of keeping it alive
        request.Version = HttpVersion.Version10;

        using var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var responseAsString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        return responseAsString;
    }
}