﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public void AddUser(User user) { throw new NotImplementedException(); }

        public User? GetUser(string userName)
        {
            return new User {Name = userName, AccountId = "000000000" };
        }

        public string[] GetUserNames() { throw new NotImplementedException(); }
    }

    private class TestWotConnector : IWotConnector
    {
        public Task<RoTData> DownloadUserDataAsync(User user, CancellationToken cancel)
        {
            var fileName = "UserDataForControllerTest.json";
            return Task.FromResult(new RoTData
            {
                CreationDate = GetCreationDate(fileName),
                Data = GetFileContent(fileName)
            });
        }
    }

    [TestMethod]
    public async Task Controller_DownloadUserData()
    {
        var services = BuildServices("testSettings.json", services =>
        {
            services
                .AddSingleton<IRoTController, RoTController>()
                .AddSingleton<IUserStore, TestUserStore>()
                .AddSingleton<IWotConnector, TestWotConnector>();
        });

        // ACTION
        var controller = services.GetRequiredService<IRoTController>();
        var result = await controller.GetUserDataAsync("User1", CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        var userData = result as WotUserData;
        Assert.IsNotNull(userData);
        Assert.AreEqual(typeof(WotUserData), result.GetType());
    }
}