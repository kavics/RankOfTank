using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RankOfTank;

namespace RankOfTankTests;

[TestClass]
public class ConnectorTests : TestBase
{
    [TestMethod]
    public async Task Connector_DownloadUserData()
    {
        var loader = Substitute.For<IDataLoader>();
        loader.LoadDataAsync(Arg.Any<Query>(), Arg.Any<User>(), CancellationToken.None)
            .Returns(x =>
            {
                var q = x[0];
                var u = (User) x[1];
                var result = new RoTData($"LoadDataAsync(query: {q}, user: \"{u.Name}:{u.AccountId}\"");
                var taskResult = Task.FromResult((RoTData?) result);
                return taskResult;
            });

        var services = BuildServices("testSettings.json", services =>
        {
            services.AddLogging(logging => logging.AddDebug());
            services.AddSingleton<IWotConnector, WotConnector>();
            services.AddSingleton(loader);
            services.AddSingleton<IDataStorage, DevNullDataStorage>();
        });

        var user = new User("User1", "12345");

        // ACTION
        var connector = services.GetRequiredService<IWotConnector>();
        var downloaded = await connector.DownloadUserDataAsync(user, CancellationToken.None);

        // ASSERT
        Assert.IsNotNull(downloaded);
        Assert.AreEqual($"LoadDataAsync(query: AccountInfo, user: \"{user.Name}:{user.AccountId}\"", downloaded.Data);
    }
}