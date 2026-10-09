using UnityEngine;

namespace MotoRush.Gameplay.Bike
{
    public class NitroController : MonoBehaviour
    {
        [Header("Nitro Settings")]
        public float nitroSpeedMultiplier = 1.5f;
        public float nitroConsumptionRate = 10f;
        public float nitroRegenRate = 2f;
        public float maxNitro = 100f;

        [Header("Effects")]
        public ParticleSystem nitroParticles;
        public MotoRush.Gameplay.Camera.CameraFollow cameraFollow;

        private float currentNitro;
        private bool isNitroActive = false;
        private BikeController bikeController;

        private void Start()
        {
            currentNitro = maxNitro;
            bikeController = GetComponent<BikeController>();
        }

        private void Update()
        {
            HandleNitro();
        }

        private void HandleNitro()
        {
            // We assume input comes from a button trigger calling SetNitroActive
            if (isNitroActive && currentNitro > 0)
            {
                currentNitro -= nitroConsumptionRate * Time.deltaTime;
                ApplyNitro();
            }
            else
            {
                currentNitro += nitroRegenRate * Time.deltaTime;
                StopNitro();
            }
            currentNitro = Mathf.Clamp(currentNitro, 0, maxNitro);
        }

        private void ApplyNitro()
        {
            bikeController.maxSpeed *= nitroSpeedMultiplier;
            if (nitroParticles != null) nitroParticles.Play();
            
            // Dynamic FOV boost
            if (cameraFollow != null)
            {
                cameraFollow.baseFOV += 10f; 
            }
        }

        private void StopNitro()
        {
            bikeController.maxSpeed /= nitroSpeedMultiplier; // Simplified logic
            if (nitroParticles != null) nitroParticles.Stop();
            
            if (cameraFollow != null)
            {
                cameraFollow.baseFOV -= 10f;
            }
        }

        public void SetNitroActive(bool active) => isNitroActive = active;
        public float GetNitroPercent() => currentNitro / maxNitro;
    }
}
