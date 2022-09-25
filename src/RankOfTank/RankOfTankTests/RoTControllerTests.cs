using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RankOfTank;

namespace RankOfTankTests;

[TestClass]
public class RoTControllerTests : TestBase
{
    [TestMethod]
    public async Task Controller_DownloadUserData()
    {
        var user = new User("User1", "000000000");
        var userStore = Substitute.For<IUserStore>();
        userStore.GetUser("User1").Returns(user);

        var rotData = new RoTData(GetFileContent("UserDataForControllerTest.json")) { CreationDate = DateTime.UtcNow };
        var connectorResult = Task.FromResult((RoTData?)rotData);
        var connector = Substitute.For<IWotConnector>();
        connector.DownloadUserDataAsync(user, CancellationToken.None).Returns(connectorResult);

        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton(userStore)
                .AddSingleton(connector);
        });

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var userData = await controller.GetUserDataAsync("User1", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNotNull(userData);
    }
    [TestMethod]
    public async Task Controller_DownloadUserData_UnknownUser()
    {
        var user = new User("User1", "000000000");
        var userStore = Substitute.For<IUserStore>();
        userStore.GetUser("User1").Returns(user);

        var rotData = new RoTData(GetFileContent("UserDataForControllerTest.json")) { CreationDate = DateTime.UtcNow };
        var connectorResult = Task.FromResult((RoTData?)rotData);
        var connector = Substitute.For<IWotConnector>();
        connector.DownloadUserDataAsync(user, CancellationToken.None).Returns(connectorResult);

        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton(userStore)
                .AddSingleton(connector);
        });

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("Unknown", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
    [TestMethod]
    public async Task Controller_DownloadUserData_NotStoredUser()
    {
        var user = new User("User1", "000042000");
        var userStore = Substitute.For<IUserStore>();
        userStore.GetUser("User1").Returns(user);

        var rotData = new RoTData(GetFileContent("UserDataForControllerTest.json")) { CreationDate = DateTime.UtcNow };
        var connectorResult = Task.FromResult((RoTData?)rotData);
        var connector = Substitute.For<IWotConnector>();
        connector.DownloadUserDataAsync(user, CancellationToken.None).Returns(connectorResult);

        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton(userStore)
                .AddSingleton(connector);
        });

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("User1", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
    [TestMethod]
    public async Task Controller_DownloadUserData_WrongDataFormat()
    {
        var user = new User("User1", "000000000");
        var userStore = Substitute.For<IUserStore>();
        userStore.GetUser("User1").Returns(user);

        var rotData = new RoTData("{}") {CreationDate = DateTime.UtcNow};
        var connectorResult = Task.FromResult((RoTData?)rotData);
        var connector = Substitute.For<IWotConnector>();
        connector.DownloadUserDataAsync(user, CancellationToken.None).Returns(connectorResult);

        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton(userStore)
                .AddSingleton(connector);
        });

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("User1", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
}