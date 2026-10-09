using UnityEngine;
using TMPro;
using MotoRush.Gameplay.Economy;

namespace MotoRush.Gameplay.UI
{
    public class HUDController : MonoBehaviour
    {
        public TextMeshProUGUI speedText;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI coinText;

        private void Update()
        {
            if (speedText != null)
            {
                // We assume the target is the bike
                float speed = FindObjectOfType<Rigidbody>().velocity.magnitude * 3.6f; // Convert m/s to km/h
                speedText.text = $"Speed: {Mathf.RoundToInt(speed)} km/h";
            }

            if (scoreText != null && ScoreManager.Instance != null)
            {
                scoreText.text = $"Score: {Mathf.RoundToInt(ScoreManager.Instance.currentScore)}";
            }

            if (coinText != null && ScoreManager.Instance != null)
            {
                coinText.text = $"Coins: {ScoreManager.Instance.coinsCollected}";
            }
        }
    }
}
