using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankOfTank;
using RankOfTank.WotModels;

namespace RankOfTankTests;

[TestClass]
public class RoTControllerTests : TestBase
{
    private class TestUserStore : IUserStore
    {
        private readonly List<User> _users = new();

        public void AddUser(User user) { _users.Add(user); }

        public User? GetUser(string userName)
        {
            return _users.FirstOrDefault(x => x.Name == userName);
        }

        public string[] GetUserNames() => _users.Select(x => x.Name).ToArray();
    }

    private class TestWotConnector : IWotConnector
    {
        public string TestFileName { get; set; }

        public Task<RoTData?> DownloadUserDataAsync(User user, CancellationToken cancel)
        {
            if (user.AccountId != "000000000")
                return Task.FromResult((RoTData?)null);

            var result = new RoTData(GetFileContent(TestFileName))
            {
                CreationDate = GetCreationDate(TestFileName)
            };
            return Task.FromResult((RoTData?)result);
        }
    }

    [TestMethod]
    public async Task Controller_DownloadUserData()
    {
        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton<IUserStore, TestUserStore>()
                .AddSingleton<IWotConnector, TestWotConnector>();
        });
        var userStore = services.GetRequiredService<IUserStore>();
        userStore.AddUser(new User("User1", "000000000"));
        var connector = (TestWotConnector)services.GetRequiredService<IWotConnector>();
        connector.TestFileName = "UserDataForControllerTest.json";


        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("User1", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        var userData = result as WotUserData;
        Assert.IsNotNull(userData);
    }
    [TestMethod]
    public async Task Controller_DownloadUserData_UnknownUser()
    {
        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton<IUserStore, TestUserStore>()
                .AddSingleton<IWotConnector, TestWotConnector>();
        });
        var userStore = services.GetRequiredService<IUserStore>();
        userStore.AddUser(new User("User1", "000000000"));
        var connector = (TestWotConnector)services.GetRequiredService<IWotConnector>();
        connector.TestFileName = "UserDataForControllerTest.json";

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("Unknown", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
    [TestMethod]
    public async Task Controller_DownloadUserData_NotStoredUser()
    {
        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton<IUserStore, TestUserStore>()
                .AddSingleton<IWotConnector, TestWotConnector>();
        });
        var userStore = services.GetRequiredService<IUserStore>();
        userStore.AddUser(new User("User1", "000042000"));
        var connector = (TestWotConnector)services.GetRequiredService<IWotConnector>();
        connector.TestFileName = "UserDataForControllerTest.json";

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("User1", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
    [TestMethod]
    public async Task Controller_DownloadUserData_WrongDataFormat()
    {
        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddLogging(logging => logging.AddDebug())
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton<IUserStore, TestUserStore>()
                .AddSingleton<IWotConnector, TestWotConnector>();
        });
        var userStore = services.GetRequiredService<IUserStore>();
        userStore.AddUser(new User("User1", "000000000"));
        var connector = (TestWotConnector)services.GetRequiredService<IWotConnector>();
        connector.TestFileName = "WrongUserDataForControllerTest.json";

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("User1", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
}