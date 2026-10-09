using UnityEngine;
using System;
using System.Collections.Generic;

namespace MotoRush.Core.SaveSystem
{
    [Serializable]
    public class PlayerData
    {
        public int TotalCoins = 0;
        public int SelectedBikeIndex = 0;
        public List<int> UnlockedBikes = new List<int> { 0 };
        public Dictionary<int, int> BikeUpgrades = new Dictionary<int, int>(); // BikeIndex, UpgradeLevel
    }

    public static class SaveManager
    {
        private static string SavePath => Application.persistentDataPath + "/playerdata.json";

        public static void Save(PlayerData data)
        {
            string json = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(SavePath, json);
        }

        public static PlayerData Load()
        {
            if (!System.IO.File.Exists(SavePath)) return new PlayerData();
            string json = System.IO.File.ReadAllText(SavePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
