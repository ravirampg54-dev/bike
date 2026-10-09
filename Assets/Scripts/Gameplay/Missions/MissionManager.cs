using UnityEngine;
using System;
using System.Collections.Generic;

namespace MotoRush.Gameplay.Missions
{
    public enum MissionType
    {
        Distance,
        Overtake,
        SpeedMaintain,
        Survival
    }

    [Serializable]
    public class Mission
    {
        public string missionName;
        public MissionType type;
        public float goalValue;
        public int rewardCoins;
        public bool isCompleted;
        public float currentProgress;

        public bool CheckCompletion()
        {
            return currentProgress >= goalValue;
        }
    }

    public class MissionManager : MonoBehaviour
    {
        public static MissionManager Instance { get; private set; }

        public List<Mission> missionList = new List<Mission>();
        public Mission activeMission;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            LoadMissions();
        }

        private void LoadMissions()
        {
            // In a real game, these would be loaded from a JSON or ScriptableObject
            missionList.Add(new Mission { missionName = "Road Trip", type = MissionType.Distance, goalValue = 1000f, rewardCoins = 100 });
            missionList.Add(new Mission { missionName = "Traffic Weaver", type = MissionType.Overtake, goalValue = 10f, rewardCoins = 200 });
            missionList.Add(new Mission { missionName = "Speed Demon", type = MissionType.SpeedMaintain, goalValue = 30f, rewardCoins = 150 });
        }

        public void UpdateMissionProgress(MissionType type, float value)
        {
            if (activeMission == null || activeMission.type != type) return;

            activeMission.currentProgress += value;
            if (activeMission.CheckCompletion())
            {
                CompleteMission();
            }
        }

        private void CompleteMission()
        {
            Debug.Log($"Mission Completed: {activeMission.missionName}!");
            activeMission.isCompleted = true;
            MotoRush.Gameplay.Economy.GarageManager.Instance.AddCoins(activeMission.rewardCoins);
            activeMission = null; // Reset active mission
        }
    }
}
