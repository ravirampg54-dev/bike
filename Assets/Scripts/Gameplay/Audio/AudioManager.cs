using UnityEngine;

namespace MotoRush.Gameplay.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        public AudioSource engineSource;
        public AudioSource sfxSource;
        public AudioSource musicSource;

        [Header("Clips")]
        public AudioClip crashClip;
        public AudioClip nitroClip;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void UpdateEnginePitch(float speed, float maxSpeed)
        {
            if (engineSource == null) return;
            float pitch = Mathf.Lerp(1f, 2f, speed / maxSpeed);
            engineSource.pitch = pitch;
        }

        public void PlaySFX(AudioClip clip)
        {
            if (sfxSource != null && clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }

        public void SetMusicVolume(float vol) => musicSource.volume = vol;
    }
}
