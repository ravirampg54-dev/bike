using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MotoRush.Gameplay.Economy;
using MotoRush.Core.SaveSystem;

namespace MotoRush.Gameplay.UI
{
    public class GarageUI : MonoBehaviour
    {
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI bikeNameText;
        public TextMeshProUGUI statsText;
        public Button buyButton;
        public Button selectButton;

        private int currentViewingIndex = 0;

        public void UpdateUI()
        {
            var profile = GarageManager.Instance.availableBikes[currentViewingIndex];
            bikeNameText.text = profile.bikeName;
            statsText.text = $"Speed: {profile.maxSpeed}\nAccel: {profile.acceleration}\nHandling: {profile.handling}";
            
            coinText.text = $"Coins: {GarageManager.Instance.GetTotalCoins()}";

            // Simple logic: check if unlocked
            // In a full implementation, we'd check SaveManager.Load().UnlockedBikes
        }

        public void NextBike()
        {
            currentViewingIndex++;
            if (currentViewingIndex >= GarageManager.Instance.availableBikes.Count) currentViewingIndex = 0;
            UpdateUI();
        }

        public void PreviousBike()
        {
            currentViewingIndex--;
            if (currentViewingIndex < 0) currentViewingIndex = GarageManager.Instance.availableBikes.Count - 1;
            UpdateUI();
        }
    }
}
