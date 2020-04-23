using System;
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
        public int CurrentSkinID { get; set; }
        public int[] CompletedChallenges { get; set; }
        public int[] UnlockedSkins { get; set; }
        public Dictionary<int, int> ChallengeProgress { get; set; }

        public GameData(Player player)
        {
            this.HighScore = player.HighScore;
            this.GoldenTiles = player.GoldenTiles;
            this.OverallJumpBoosts = player.OverallJumpBoosts;
            this.CurrentSkinID = player.CurrentSkinID;
            this.CompletedChallenges = player.CompletedChallanges.ToArray();
            this.UnlockedSkins = player.UnlockedSkins.ToArray();
            this.ChallengeProgress = getChallengeProgress(player);
        }

        public GameData()
        {
            this.HighScore = 0;
            this.GoldenTiles = 0;
            this.OverallJumpBoosts = 0;
            this.CompletedChallenges = new int[0];
            this.UnlockedSkins = new int[0];
            this.ChallengeProgress = new Dictionary<int, int>();
        }

        Dictionary<int, int> getChallengeProgress(Player player)
        {
            Dictionary<int, int> challengeProgress = new Dictionary<int, int>();
            foreach (Challenge challenge in player.ChallengeProgress)
            {
                challengeProgress.Add(challenge.ID, challenge.Progress);
            }
            return challengeProgress;
        }
    }
}
