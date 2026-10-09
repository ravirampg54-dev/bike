using UnityEngine;
using System.Collections.Generic;

namespace MotoRush.Gameplay.Traffic
{
    public class TrafficSpawner : MonoBehaviour
    {
        [Header("Settings")]
        public GameObject[] vehiclePrefabs;
        public float spawnInterval = 2f;
        public float spawnDistance = 100f;
        public float roadWidth = 10f;
        public int laneCount = 3;

        private float timer;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnVehicle();
                timer = 0;
            }
        }

        private void SpawnVehicle()
        {
            // Random lane selection
            float laneWidth = roadWidth / laneCount;
            float randomLane = Random.Range(0, laneCount);
            float xPos = (-roadWidth / 2) + (randomLane * laneWidth) + (laneWidth / 2);

            Vector3 spawnPos = transform.position + new Vector3(xPos, 0, spawnDistance);
            
            GameObject vehicleGo = Instantiate(vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)], spawnPos, Quaternion.identity);
            TrafficVehicle vehicle = vehicleGo.GetComponent<TrafficVehicle>();
            
            if (vehicle != null)
            {
                // Give vehicles slightly different speeds
                vehicle.SetSpeed(Random.Range(10f, 25f));
            }
        }
    }
}
