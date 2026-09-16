using Project.Scripts.Config;
using UnityEngine;

namespace Project.Scripts.Audio
{
    public class AudioInit : MonoBehaviour
    {
        [SerializeField] private SoundConfig _config;

        private SoundService _service;

        public SoundService Service
        {
            get
            {
                if (_service == null)
                    _service = CreateService();
                return _service;
            }
        }

        private SoundService CreateService()
        {
            var sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.spatialBlend = 0f;

            var musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.playOnAwake = false;
            musicSource.spatialBlend = 0f;

            return new SoundService(sfxSource, musicSource, _config);
        }
    }
}