using UnityEngine;

namespace MotoRush.Gameplay.Bike
{
    public class BikeInputHandler : MonoBehaviour
    {
        public static BikeInputHandler Instance { get; private set; }

        public bool IsAccelerating { get; private set; }
        public bool IsBraking { get; private set; }
        public float SteeringInput { get; private set; } // -1 for left, 1 for right

        private void Awake()
        {
            Instance = this;
        }

        public void SetAcceleration(bool active) => IsAccelerating = active;
        public void SetBraking(bool active) => IsBraking = active;
        public void SetSteering(float value) => SteeringInput = value;
    }
}
