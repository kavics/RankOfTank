// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
#pragma warning disable CS8618
namespace RankOfTank.WotModels
{
    public class WotUserData
    {
        /// <summary>Language selected in the game client.</summary>
        public string client_language { get; set; }
        /// <summary>Last battle time.</summary>
        public long last_battle_time { get; set; }
        /// <summary>Player account ID. Number part of the your Wargaming.net OpenID
        /// (e.g. https://eu.wargaming.net/id/&lt;numbers&gt;-&lt;nickname&gt;)</summary>
        public long account_id { get; set; }
        /// <summary>Clan ID.</summary>
        public object clan_id { get; set; }
        /// <summary>Date when player's account was created.</summary>
        public long created_at { get; set; }
        /// <summary>Date when player details were updated.</summary>
        public long updated_at { get; set; }
        /// <summary>Player's private data.</summary>
        public object @private { get; set; }
        /// <summary>Personal rating.</summary>
        public int global_rating { get; set; }
        /// <summary>End time of last game session.</summary>
        public long logout_at { get; set; }
        /// <summary>Player name.</summary>
        public string nickname { get; set; }
        /// <summary>Player statistics.</summary>
        public WotStatistics statistics { get; set; }
    }
}
