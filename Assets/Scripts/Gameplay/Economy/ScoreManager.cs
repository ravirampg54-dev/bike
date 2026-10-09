using UnityEngine;

namespace MotoRush.Gameplay.Economy
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        public float currentScore { get; private set; }
        public int coinsCollected { get; private set; }
        public float maxSpeedReached { get; private set; }

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void AddScore(float amount)
        {
            currentScore += amount;
        }

        public void AddCoins(int amount)
        {
            coinsCollected += amount;
        }

        public void UpdateMaxSpeed(float speed)
        {
            if (speed > maxSpeedReached) maxSpeedReached = speed;
        }
    }
}
