using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankOfTank;

namespace RankOfTankTests;

[TestClass]
public class InMemoryDataStorageTests : TestBase
{
    [TestMethod]
    public async Task DataStorage_InMem_LoadFromEmpty()
    {
        var storage = new InMemoryDataStorage();

        // ACTION
        var user = new User("User1", "000000000");
        var result = await storage.LoadDataAsync(Query.AccountInfo, user, CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
    [TestMethod]
    public async Task DataStorage_InMem_SaveAndLoadBack()
    {
        var storage = new InMemoryDataStorage();
        var query = Query.AccountInfo;
        var user = new User("User1", "000000000");
        var data = new RoTData("Data1") { CreationDate = DateTime.UtcNow.AddHours(-1) };

        // ACTION
        await storage.SaveDataAsync(query, user, data, CancellationToken.None).ConfigureAwait(false);
        var result = await storage.LoadDataAsync(query, user, CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNotNull(result);
        Assert.AreEqual("Data1", result.Data);
    }
    [TestMethod]
    public async Task DataStorage_InMem_LoadLast()
    {
        var storage = new InMemoryDataStorage();
        var query = Query.AccountInfo;
        var user = new User("User1", "000000000");
        var date1 = DateTime.UtcNow.AddHours(-5);
        var date2 = date1.AddHours(1);
        var date3 = date2.AddHours(1);

        var data1 = new RoTData("Data1") { CreationDate = date1 };
        var data2 = new RoTData("Data2") { CreationDate = date2 };
        var data3 = new RoTData("Data3") { CreationDate = date3 };

        await storage.SaveDataAsync(query, user, data1, CancellationToken.None).ConfigureAwait(false);
        await storage.SaveDataAsync(query, user, data2, CancellationToken.None).ConfigureAwait(false);
        await storage.SaveDataAsync(query, user, data3, CancellationToken.None).ConfigureAwait(false);

        // ACTION
        var result = await storage.LoadDataAsync(query, user, CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNotNull(result);
        Assert.AreEqual("Data3", result.Data);
        Assert.AreEqual(date3, result.CreationDate);
    }
}

