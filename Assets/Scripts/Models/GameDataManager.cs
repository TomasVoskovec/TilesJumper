using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Models
{
    public static class GameDataManager
    {
        public static void Save (Player player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/gameData.tj";
            FileStream fileStream = new FileStream(path, FileMode.Create);

            GameData gameData = new GameData(player);

            formatter.Serialize(fileStream, gameData);
            fileStream.Close();
        }

        public static GameData Load()
        {
            string path = Application.persistentDataPath + "/gameData.tj";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(path, FileMode.Open);

                GameData gameData = (GameData)formatter.Deserialize(fileStream);
                fileStream.Close();

                return gameData;
            }
            return null;
        }

        public static GameData Restore()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/gameData.tj";
            FileStream fileStream = new FileStream(path, FileMode.Create);

            GameData gameData = new GameData();

            formatter.Serialize(fileStream, gameData);
            fileStream.Close();

            return gameData;
        }
    }
}
