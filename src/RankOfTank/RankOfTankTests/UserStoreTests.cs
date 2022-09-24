using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankOfTank;

namespace RankOfTankTests;

[TestClass]
public class UserStoreTests : TestBase
{
    private ServiceProvider BuildServices()
    {
        return base.BuildServices("testSettings.json", services =>
        {
            services.AddSingleton<IUserStore, UserStore>();
        });
    }

    [TestMethod]
    public void UserStore_BuiltInUsers()
    {
        // ALIGN
        var services = BuildServices();

        // ACTION
        var userStore = services.GetRequiredService<IUserStore>();
        var userNames = userStore.GetUserNames().OrderBy(x => x).ToArray();

        // ASSERT
        Assert.AreEqual(2, userNames.Length);
        Assert.AreEqual("User1", userNames[0]);
        Assert.AreEqual("User2", userNames[1]);
        Assert.AreEqual("123456789", userStore.GetUser("User1")?.AccountId);
        Assert.AreEqual("987654321", userStore.GetUser("User2")?.AccountId);
    }
    [TestMethod]
    public void UserStore_UnknownUser()
    {
        // ALIGN
        var services = BuildServices();

        // ACTION
        var userStore = services.GetRequiredService<IUserStore>();
        var user = userStore.GetUser("unknown");

        // ASSERT
        Assert.IsNull(user);
    }
    [TestMethod]
    public void UserStore_AddUser()
    {
        // ALIGN
        var services = BuildServices();

        // ACTION
        var userStore = services.GetRequiredService<IUserStore>();
        userStore.AddUser(new User("User3", "123454321"));

        // ASSERT
        var userNames = userStore.GetUserNames().OrderBy(x => x).ToArray();
        Assert.AreEqual(3, userNames.Length);
        Assert.AreEqual("User1", userNames[0]);
        Assert.AreEqual("User2", userNames[1]);
        Assert.AreEqual("User3", userNames[2]);
        Assert.AreEqual("123456789", userStore.GetUser("User1")?.AccountId);
        Assert.AreEqual("987654321", userStore.GetUser("User2")?.AccountId);
        Assert.AreEqual("123454321", userStore.GetUser("User3")?.AccountId);
    }
    [TestMethod]
    public void UserStore_AddUser_Twice()
    {
        // ALIGN
        var services = BuildServices();

        // ACTION
        var userStore = services.GetRequiredService<IUserStore>();
        userStore.AddUser(new User("User3", "123454321"));
        userStore.AddUser(new User("User3", "987656789"));

        // ASSERT
        var userNames = userStore.GetUserNames().OrderBy(x => x).ToArray();
        Assert.AreEqual(3, userNames.Length);
        Assert.AreEqual("User1", userNames[0]);
        Assert.AreEqual("User2", userNames[1]);
        Assert.AreEqual("User3", userNames[2]);
        Assert.AreEqual("123456789", userStore.GetUser("User1")?.AccountId);
        Assert.AreEqual("987654321", userStore.GetUser("User2")?.AccountId);
        Assert.AreEqual("987656789", userStore.GetUser("User3")?.AccountId);
    }
}