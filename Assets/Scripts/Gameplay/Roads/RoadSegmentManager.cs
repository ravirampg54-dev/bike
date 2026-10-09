using UnityEngine;
using System.Collections.Generic;

namespace MotoRush.Gameplay.Roads
{
    public class RoadSegmentManager : MonoBehaviour
    {
        [Header("Settings")]
        public GameObject roadPrefab;
        public int segmentCount = 5;
        public float segmentLength = 50f;

        private List<GameObject> activeSegments = new List<GameObject>();
        private float spawnZ = 0f;

        private void Start()
        {
            for (int i = 0; i < segmentCount; i++)
            {
                SpawnSegment();
            }
        }

        public void UpdateRoad(Vector3 playerPosition)
        {
            if (playerPosition.z > (spawnZ - (segmentCount * segmentLength)))
            {
                SpawnSegment();
                RemoveOldSegment();
            }
        }

        private void SpawnSegment()
        {
            GameObject go = Instantiate(roadPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
            activeSegments.Add(go);
            spawnZ += segmentLength;
        }

        private void RemoveOldSegment()
        {
            if (activeSegments.Count > 0)
            {
                GameObject oldSegment = activeSegments[0];
                activeSegments.RemoveAt(0);
                Destroy(oldSegment); 
                // Note: In final optimization, we will use a Pool instead of Destroy
            }
        }
    }
}
