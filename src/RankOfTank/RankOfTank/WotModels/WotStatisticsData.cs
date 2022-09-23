using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankOfTank.WotModels
{
    public class WotStatisticsData
    {
        /// <summary>Enemies spotted</summary>
        public int spotted { get; set; }
        /// <summary>Average damage upon your shooting the track. Value is calculated starting from version 8.8.</summary>
        public float avg_damage_assisted_track { get; set; }
        /// <summary>Average damage blocked by armor per battle. Damage blocked by armor is damage received from shells (AP, HEAT and APCR) that hit a vehicle but caused no damage. Value is calculated starting from version 9.0.</summary>
        public float avg_damage_blocked { get; set; }
        /// <summary>Direct hits received</summary>
        public int direct_hits_received { get; set; }
        /// <summary>Hits on enemy as a result of splash damage</summary>
        public int explosion_hits { get; set; }
        /// <summary>Penetrations received</summary>
        public int piercings_received { get; set; }
        /// <summary>Penetrations</summary>
        public int piercings { get; set; }
        /// <summary>Total experience</summary>
        public int xp { get; set; }
        /// <summary>Battles survived</summary>
        public int survived_battles { get; set; }
        /// <summary>Base defense points</summary>
        public int dropped_capture_points { get; set; }
        /// <summary>Hit ratio</summary>
        public int hits_percents { get; set; }
        /// <summary>Draws</summary>
        public int draws { get; set; }
        /// <summary>Battles fought</summary>
        public int battles { get; set; }
        /// <summary>Damage received</summary>
        public int damage_received { get; set; }
        /// <summary>Average damage caused with your assistance. Value is calculated starting from version 8.8.</summary>
        public float avg_damage_assisted { get; set; }
        /// <summary>Vehicles destroyed</summary>
        public int frags { get; set; }
        /// <summary>Average damage upon your spotting. Value is calculated starting from version 8.8.</summary>
        public float avg_damage_assisted_radio { get; set; }
        /// <summary>Base capture points</summary>
        public int capture_points { get; set; }
        /// <summary>Experience earned in battles without Premium. Value is calculated starting from version 9.0. Warning. The field will be disabled.</summary>
        public int base_xp { get; set; }
        /// <summary>Hits</summary>
        public int hits { get; set; }
        /// <summary>Average experience per battle</summary>
        public int battle_avg_xp { get; set; }
        /// <summary>Victories</summary>
        public int wins { get; set; }
        /// <summary>Defeats</summary>
        public int losses { get; set; }
        /// <summary>Damage caused</summary>
        public int damage_dealt { get; set; }
        /// <summary>Direct hits received that caused no damage</summary>
        public int no_damage_direct_hits_received { get; set; }
        /// <summary>Shots fired</summary>
        public int shots { get; set; }
        /// <summary>Hits received as a result of splash damage</summary>
        public int explosion_hits_received { get; set; }
        /// <summary>Ratio of damage blocked by armor from AP, HEAT, and APCR shells to damage received from these types of shells. Value is calculated starting from version 9.0.</summary>
        public float tanking_factor { get; set; }
    }
}
