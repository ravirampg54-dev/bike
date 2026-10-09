using UnityEngine;
using TMPro;

namespace MotoRush.Gameplay.UI
{
    public class MissionUI : MonoBehaviour
    {
        public TextMeshProUGUI missionText;
        public TextMeshProUGUI progressText;

        private void Update()
        {
            if (MotoRush.Gameplay.Missions.MissionManager.Instance == null) return;
            
            var active = MotoRush.Gameplay.Missions.MissionManager.Instance.activeMission;
            if (active != null)
            {
                missionText.text = $"Mission: {active.missionName}";
                progressText.text = $"{active.currentProgress:F0} / {active.goalValue:F0}";
            }
            else
            {
                missionText.text = "No Active Mission";
                progressText.text = "";
            }
        }
    }
}
