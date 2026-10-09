using UnityEngine;

namespace MotoRush.Gameplay.Traffic
{
    public class TrafficVehicle : MonoBehaviour
    {
        public float speed = 15f;
        private bool isActive = true;

        public void SetSpeed(float newSpeed) => speed = newSpeed;

        private void Update()
        {
            if (!isActive) return;
            
            // Traffic moves forward in the same direction as player but slower
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void Deactivate()
        {
            isActive = false;
            // Logic for returning to pool will go here
        }
    }
}
