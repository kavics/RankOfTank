using Newtonsoft.Json;

namespace RankOfTank.WotModels;

internal class WotGarageDataResponseItem
{
    [JsonProperty("tank_id")]
    public int TankId { get; set; }
}
public class WotGarageData
{
    public int[] TankIds { get; set; } = Array.Empty<int>();
}
