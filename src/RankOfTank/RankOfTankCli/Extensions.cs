namespace RankOfTankCli;

public static class Extensions
{
    /// <summary>Returns UTC DateTime from the given Wargaming timestamp.</summary>
    public static DateTime ToDateTime(this long wgTimestamp)
    {
        var unixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return unixStartDate.AddSeconds(wgTimestamp);

    }
}

