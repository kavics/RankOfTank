using Microsoft.VisualStudio.TestTools.UnitTesting;
using RankOfTank;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using RankOfTankTests.Infrastructure;

namespace RankOfTankTests;

[TestClass]
public class FsDataStorageTests
{
    private string? _databaseDirectory;
#pragma warning disable CS8618
    private IServiceProvider _services;
#pragma warning restore CS8618

#pragma warning disable CS8618
    public TestContext TestContext { get; set; }
#pragma warning restore CS8618

    [TestInitialize]
    public void InitializeTest()
    {
        _databaseDirectory = Path.Combine(TestContext.TestRunDirectory, TestContext.TestName) ;

        _services = new ServiceCollection()
            .AddFsDataStorage(options =>
            {
                options.DatabaseDirectory = _databaseDirectory;
            })
            .BuildServiceProvider();
    }

    [TestMethod]
    public void DataStorage_Fs_TestInfrastructure()
    {
        // Checks InitializeTest method (_databaseDirectory, _services)
        var storage = _services.GetRequiredService<IDataStorage>();
        var storageAcc = new ObjectAccessor(storage);
        var options = (FsDataStorageOptions)storageAcc.GetField("_options");
        Assert.IsNotNull(options);
        Assert.AreEqual(_databaseDirectory, options.DatabaseDirectory);
    }

    [TestMethod]
    public async Task DataStorage_Fs_LoadFromEmpty()
    {
        var storage = _services.GetRequiredService<IDataStorage>();

        // ACTION
        var user = new User("User1", "000000000");
        var result = await storage.LoadDataAsync(Query.AccountInfo, user, CancellationToken.None).ConfigureAwait(false);

        // ASSERT
        Assert.IsNull(result);
    }
    [TestMethod]
    public async Task DataStorage_Fs_SaveAndLoadBack()
    {
        var storage = _services.GetRequiredService<IDataStorage>();

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
    public async Task DataStorage_Fs_LoadLast()
    {
        var storage = _services.GetRequiredService<IDataStorage>();

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
