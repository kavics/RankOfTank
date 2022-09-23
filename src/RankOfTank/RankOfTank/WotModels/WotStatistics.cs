namespace RankOfTank.WotModels
{
    public class WotStatistics
    {
        /// <summary>Vehicle, in which maximum number of enemy vehicles was destroyed.</summary>
        public int? max_frags_tank_id { get; set; }
        /// <summary>Vehicle used to gain maximum experience per battle.</summary>
        public int? max_xp_tank_id { get; set; }
        /// <summary>Maximumax_damagem experience per battle.</summary>
        public int max_xp { get; set; }
        /// <summary>Trees knocked down.</summary>
        public int trees_cut { get; set; }
        /// <summary>Maximum destroyed in battle.</summary>
        public int max_frags { get; set; }
        /// <summary>Vehicle used to cause maximum damage.</summary>
        public int? max_damage_tank_id { get; set; }
        /// <summary>Number and models of vehicles destroyed by a player. Player's private data.</summary>
        public object frags { get; set; }
        /// <summary>Maximum damage caused per battle.</summary>
        public int max_damage { get; set; }
        /// <summary>Vehicle used to cause maximum damage. Warning. The field will be disabled.</summary>
        public int? max_damage_vehicle { get; set; }
        /// <summary>Clan battles statistics.</summary>
        public WotStatisticsData clan { get; set; }
        /// <summary>Tank Company battles statistics.</summary>
        public WotStatisticsData company { get; set; }
        /// <summary>Overall Statistics.</summary>
        public WotStatisticsData all { get; set; }
        /// <summary>General stats for player's battles in Stronghold defense.</summary>
        public WotStatisticsData stronghold_defense { get; set; }
        /// <summary>General stats for player's battles in Skirmishes.</summary>
        public WotStatisticsData stronghold_skirmish { get; set; }
        /// <summary>Historical battles statistics.</summary>
        public WotStatisticsData historical { get; set; }
        /// <summary>Team battles statistics.</summary>
        public WotStatisticsData team { get; set; }
    }
}
