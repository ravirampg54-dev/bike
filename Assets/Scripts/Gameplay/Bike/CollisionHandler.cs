using UnityEngine;
using MotoRush.Core;
using MotoRush.Gameplay.Economy;

namespace MotoRush.Gameplay.Bike
{
    public class CollisionHandler : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Traffic") || collision.gameObject.CompareTag("Wall"))
            {
                HandleCrash();
            }
        }

        private void HandleCrash()
        {
            Debug.Log("CRASHED!");
            
            if (MotoRush.Gameplay.Audio.AudioManager.Instance != null)
            {
                MotoRush.Gameplay.Audio.AudioManager.Instance.PlaySFX(MotoRush.Gameplay.Audio.AudioManager.Instance.crashClip);
            }

            GameManager.Instance.UpdateState(GameState.GameOver);
            
            // Disable movement
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
    }
}
