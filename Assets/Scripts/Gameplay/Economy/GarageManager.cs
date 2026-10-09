using UnityEngine;
using System.Collections.Generic;
using MotoRush.Core.SaveSystem;

namespace MotoRush.Gameplay.Economy
{
    public class BikeStats
    {
        public string BikeName;
        public float BaseMaxSpeed;
        public float BaseAcceleration;
        public float BaseHandling;
        public int PurchasePrice;
    }

    [System.Serializable]
    public class BikeProfile
    {
        public string bikeName;
        public float maxSpeed;
        public float acceleration;
        public float handling;
        public int price;
    }

    public class GarageManager : MonoBehaviour
    {
        public static GarageManager Instance { get; private set; }

        public List<BikeProfile> availableBikes;
        private PlayerData playerData;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            playerData = SaveManager.Load();
        }

        public BikeProfile GetSelectedBike()
        {
            int index = playerData.SelectedBikeIndex;
            return availableBikes[index];
        }

        public void PurchaseBike(int index)
        {
            if (playerData.UnlockedBikes.Contains(index)) return;

            int price = availableBikes[index].price;
            if (playerData.TotalCoins >= price)
            {
                playerData.TotalCoins -= price;
                playerData.UnlockedBikes.Add(index);
                SaveManager.Save(playerData);
            }
        }

        public void SelectBike(int index)
        {
            if (playerData.UnlockedBikes.Contains(index))
            {
                playerData.SelectedBikeIndex = index;
                SaveManager.Save(playerData);
            }
        }

        public int GetTotalCoins() => playerData.TotalCoins;
        public void AddCoins(int amount)
        {
            playerData.TotalCoins += amount;
            SaveManager.Save(playerData);
        }
    }
}
