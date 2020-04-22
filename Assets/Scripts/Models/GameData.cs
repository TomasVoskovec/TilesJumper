﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class GameData
    {
        public int HighScore { get; set; }
        public int GoldenTiles { get; set; }
        public int OverallJumpBoosts { get; set; }
        public int[] CompletedChallanges { get; set; }
        public int[] UnlockedSkins { get; set; }

        public GameData(Player player)
        {
            this.HighScore = player.HighScore;
            this.GoldenTiles = player.GoldenTiles;
            this.OverallJumpBoosts = player.OverallJumpBoosts;
            this.CompletedChallanges = player.CompletedChallanges.ToArray();
            this.UnlockedSkins = player.UnlockedSkins.ToArray();
        }

        public GameData()
        {
            this.HighScore = 0;
            this.GoldenTiles = 0;
            this.OverallJumpBoosts = 0;
            this.CompletedChallanges = new int[0];
            this.UnlockedSkins = new int[0];
        }
    }
}
