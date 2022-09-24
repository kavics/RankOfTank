using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankOfTank;

namespace RankOfTankTests;

[TestClass]
public class ConnectorTests : TestBase
{
    private class TestDataLoader : IDataLoader
    {
        public Task<RoTData?> LoadDataAsync(Query query, User user, CancellationToken cancel)
        {
            var result = new RoTData($"LoadDataAsync(query: {query}, user: \"{user.Name}:{user.AccountId}\"");
            return Task.FromResult((RoTData?)result);
        }
    }

    [TestMethod]
    public async Task Connector_DownloadUserData()
    {
        var services = BuildServices("testSettings.json", services =>
        {
            services.AddLogging(logging => logging.AddDebug());
            services.AddSingleton<IWotConnector, WotConnector>();
            services.AddSingleton<IDataLoader, TestDataLoader>();
            services.AddSingleton<IDataStorage, DevNullDataStorage>();
        });

        var user = new User("User1", "1234");

        // ACTION
        var connector = services.GetRequiredService<IWotConnector>();
        var downloaded = await connector.DownloadUserDataAsync(user, CancellationToken.None);

        // ASSERT
        Assert.IsNotNull(downloaded);
        Assert.AreEqual($"LoadDataAsync(query: AccountInfo, user: \"{user.Name}:{user.AccountId}\"", downloaded.Data);
    }
}