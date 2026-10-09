using UnityEngine;
using MotoRush.Gameplay.Bike;

namespace MotoRush.Gameplay.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Follow Settings")]
        public Transform target;
        public Vector3 offset = new Vector3(0, 3, -6);
        public float smoothSpeed = 0.125f;

        [Header("Dynamic FOV")]
        public Camera cam;
        public float baseFOV = 60f;
        public float maxFOV = 80f;
        public float speedForMaxFOV = 50f;

        private void LateUpdate()
        {
            if (target == null) return;

            // Smooth Position Follow
            Vector3 desiredPosition = target.position + target.transform.rotation * offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Smooth Rotation Follow
            transform.LookAt(target);

            // Dynamic FOV based on speed
            if (cam != null)
            {
                float speed = target.GetComponent<Rigidbody>().velocity.magnitude;
                float fovMultiplier = Mathf.Clamp01(speed / speedForMaxFOV);
                cam.fieldOfView = Mathf.Lerp(baseFOV, maxFOV, fovMultiplier);
            }
        }
    }
}
