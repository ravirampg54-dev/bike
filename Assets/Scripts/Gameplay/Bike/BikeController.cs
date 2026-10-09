using UnityEngine;
using MotoRush.Core;

namespace MotoRush.Gameplay.Bike
{
    [RequireComponent(typeof(Rigidbody))]
    public class BikeController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float accelerationForce = 25f;
        public float maxSpeed = 50f;
        public float brakingForce = 30f;
        public float steeringSpeed = 10f;
        public float leanAngle = 20f;

    [Header("References")]
    public Transform bikeMesh;
    public MotoRush.Gameplay.Roads.RoadSegmentManager roadManager;
    public MotoRush.Gameplay.Traffic.TrafficSpawner trafficSpawner;

        private Rigidbody rb;
        private BikeInputHandler input;
        private float currentSpeed;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = FindObjectOfType<BikeInputHandler>();
            
            // Load stats from Garage
            if (MotoRush.Gameplay.Economy.GarageManager.Instance != null)
            {
                var profile = MotoRush.Gameplay.Economy.GarageManager.Instance.GetSelectedBike();
                maxSpeed = profile.maxSpeed;
                accelerationForce = profile.acceleration;
                steeringSpeed = profile.handling;
            }

            rb.drag = 0.5f;
            rb.angularDrag = 2f;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance?.CurrentState != GameState.Racing) return;

            HandleMovement();
            HandleSteering();

            if (roadManager != null)
            {
                roadManager.UpdateRoad(transform.position);
            }

            // Update score based on distance and speed
            if (MotoRush.Gameplay.Economy.ScoreManager.Instance != null)
            {
                float speed = rb.velocity.magnitude;
                MotoRush.Gameplay.Economy.ScoreManager.Instance.AddScore(speed * Time.fixedDeltaTime);
                MotoRush.Gameplay.Economy.ScoreManager.Instance.UpdateMaxSpeed(speed);
                
                // Update Distance Mission
                if (MotoRush.Gameplay.Missions.MissionManager.Instance != null)
                {
                    MotoRush.Gameplay.Missions.MissionManager.Instance.UpdateMissionProgress(
                        MotoRush.Gameplay.Missions.MissionType.Distance, 
                        speed * Time.fixedDeltaTime
                    );
                }
            }

            // Update Audio Pitch
            if (MotoRush.Gameplay.Audio.AudioManager.Instance != null)
            {
                MotoRush.Gameplay.Audio.AudioManager.Instance.UpdateEnginePitch(rb.velocity.magnitude, maxSpeed);
            }
        }

        private void HandleMovement()
        {
            if (input.IsAccelerating)
            {
                rb.AddForce(transform.forward * accelerationForce, ForceMode.Acceleration);
            }
            else if (input.IsBraking)
            {
                rb.AddForce(-transform.forward * brakingForce, ForceMode.Acceleration);
            }

            currentSpeed = rb.velocity.magnitude;

            if (currentSpeed > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }

        private void HandleSteering()
        {
            float steer = input.SteeringInput;
            
            // Apply rotation to the Rigidbody
            Quaternion deltaRotation = Quaternion.Euler(0f, steer * steeringSpeed * Time.fixedDeltaTime * 50f, 0f);
            rb.MoveRotation(rb.rotation * deltaRotation);

            // Visual Lean
            if (bikeMesh != null)
            {
                float targetLean = -steer * leanAngle;
                Quaternion targetRot = Quaternion.Euler(0f, 0f, targetLean);
                bikeMesh.localRotation = Quaternion.Slerp(bikeMesh.localRotation, targetRot, Time.fixedDeltaTime * 5f);
            }
        }
    }
}
