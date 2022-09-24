using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankOfTank;

namespace RankOfTankTests;

[TestClass]
public class ConnectorTests : TestBase
{
    private class TestDataLoader : IDataLoader
    {
        public Task<RoTData> LoadDataAsync(Query query, User user, CancellationToken cancel)
        {
            return Task.FromResult(new RoTData
                {
                    CreationDate = DateTime.UtcNow,
                    Data = $"LoadDataAsync(query: {query}, user: \"{user.Name}:{user.AccountId}\""
                }
            );
        }
    }

    [TestMethod]
    public async Task Connector_DownloadUserData()
    {
        var services = BuildServices("testSettings.json", services =>
        {
            services.AddSingleton<IWotConnector, WotConnector>();
            services.AddSingleton<IDataLoader, TestDataLoader>();
            services.AddSingleton<IDataStorage, DevNullDataStorage>();
        });

        var user = new User {Name = "User1", AccountId = "1234"};

        // ACTION
        var connector = services.GetRequiredService<IWotConnector>();
        var downloaded = await connector.DownloadUserDataAsync(user, CancellationToken.None);

        // ASSERT
        Assert.AreEqual($"LoadDataAsync(query: AccountInfo, user: \"{user.Name}:{user.AccountId}\"", downloaded.Data);
    }
}