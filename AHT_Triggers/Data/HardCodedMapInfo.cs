using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHT_Triggers.Data
{
    /// <summary>
    /// Map information that is defined in the game's executable instead of the map file.
    /// </summary>
    public static class MapInfo
    {
        public struct HardCodedMapInfo
        {
            public string MapName;
            public bool HasOwnRange;
            public float DefaultTriggerRange;
        }

        /// <summary>
        /// For any map that doesn't define its own default trigger range, its range is derived from its base class.
        /// </summary>
        public static readonly float BASE_DEFAULT_TRIGGER_RANGE = 50f;

        /// <summary>
        /// Hard-coded information about every map. Key is the map's file name (without .edb extension). PLEASE REFACTOR ME!!!
        /// </summary>
        public static readonly Dictionary<string, HardCodedMapInfo> MAP_FILE_INFO = new Dictionary<string, HardCodedMapInfo>()
        {
            ["realm1a"] = new HardCodedMapInfo {
                MapName = "Map_DragonVillage",
                HasOwnRange = true,
                DefaultTriggerRange = 70f
            },
            ["realm1b"] = new HardCodedMapInfo()
            {
                MapName = "Map_DragonSwamp",
                HasOwnRange = true,
                DefaultTriggerRange = 70f
            },
            ["realm1c"] = new HardCodedMapInfo()
            {
                MapName = "Map_DragonShores",
                HasOwnRange = true,
                DefaultTriggerRange = 80f
            },
            ["realm3a"] = new HardCodedMapInfo()
            {
                MapName = "Map_FrostbiteVillage",
                HasOwnRange = true,
                DefaultTriggerRange = 50f
            },
            ["realm4e"] = new HardCodedMapInfo()
            {
                MapName = "Map_ProfessorsLab",
                HasOwnRange = true,
                DefaultTriggerRange = 70f
            },
            ["realm2a"] = new HardCodedMapInfo()
            {
                MapName = "Map_Beach",
                HasOwnRange = true,
                DefaultTriggerRange = 70f
            },
            ["test_js"] = new HardCodedMapInfo()
            {
                MapName = "Map_Test_js",
                HasOwnRange = true,
                DefaultTriggerRange = 10f
            },
            ["test_ka"] = new HardCodedMapInfo()
            {
                MapName = "Map_Test_ka",
                HasOwnRange = true,
                DefaultTriggerRange = 70f
            },
            ["test_wts"] = new HardCodedMapInfo()
            {
                MapName = "Map_Test_wts",
                HasOwnRange = true,
                DefaultTriggerRange = 70f
            },
            ["realm1z"] = new HardCodedMapInfo()
            {
                MapName = "Map_GnastyGnorc",
                HasOwnRange = true,
                DefaultTriggerRange = 90f
            },
            ["realm3z"] = new HardCodedMapInfo()
            {
                MapName = "Map_RedsChamber",
                HasOwnRange = true,
                DefaultTriggerRange = 1000f
            },
            ["playroom"] = new HardCodedMapInfo()
            {
                MapName = "Map_PlayRoom",
                HasOwnRange = true,
                DefaultTriggerRange = 50f
            },
            ["model"] = new HardCodedMapInfo()
            {
                MapName = "Map_ModelViewer",
                HasOwnRange = true,
                DefaultTriggerRange = 50f
            },
            ["mr1_spy"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR1_Spy",
                HasOwnRange = true,
                DefaultTriggerRange = 50f
            },
            ["mr1_blk"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR1_Blk",
                HasOwnRange = true,
                DefaultTriggerRange = 80f
            },
            ["mr1_sgt"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR1_Sgt",
                HasOwnRange = true,
                DefaultTriggerRange = 120f
            },
            ["mr1_spx"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR1_Spx",
                HasOwnRange = true,
                DefaultTriggerRange = 200f
            },
            ["mr2_blk"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR2_Blk",
                HasOwnRange = true,
                DefaultTriggerRange = 80f
            },
            ["mr2_sgt"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR2_Sgt",
                HasOwnRange = true,
                DefaultTriggerRange = 120f
            },
            ["mr2_spx"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR2_Spx",
                HasOwnRange = true,
                DefaultTriggerRange = 200f
            },
            ["mr2_spy"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR2_Spy",
                HasOwnRange = true,
                DefaultTriggerRange = 300f
            },
            ["mr3_blk"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR3_Blk",
                HasOwnRange = true,
                DefaultTriggerRange = 80f
            },
            ["mr3_sgt"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR3_Sgt",
                HasOwnRange = true,
                DefaultTriggerRange = 80f
            },
            ["mr3_spx"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR3_Spx",
                HasOwnRange = true,
                DefaultTriggerRange = 200f
            },
            ["mr3_spy"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR3_Spy",
                HasOwnRange = true,
                DefaultTriggerRange = 80f
            },
            ["mr4_blk"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR4_Blk",
                HasOwnRange = true,
                DefaultTriggerRange = 80f
            },
            ["mr4_sgt"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR4_Sgt",
                HasOwnRange = true,
                DefaultTriggerRange = 120f
            },
            ["mr4_spx"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR4_Spx",
                HasOwnRange = true,
                DefaultTriggerRange = 200f
            },
            ["mr4_spy"] = new HardCodedMapInfo()
            {
                MapName = "Map_MiniGame_MR4_Spy",
                HasOwnRange = true,
                DefaultTriggerRange = 300f
            }
        };
    };
}
