using Project.Scripts.Config;
using Project.Scripts.Sound;
using UnityEngine;

namespace Project.Scripts.Audio
{
    public class SoundService
    {
        private readonly AudioSource _sfxSource;
        private readonly AudioSource _musicSource;
        private readonly SoundConfig _config;

        public SoundService(AudioSource sfxSource, AudioSource musicSource, SoundConfig config)
        {
            _sfxSource = sfxSource;
            _musicSource = musicSource;
            _config = config;

            if (_config.MusicClip != null)
            {
                _musicSource.clip = _config.MusicClip;
                _musicSource.loop = true;
                _musicSource.volume = _config.MusicVolume;
                _musicSource.Play();
            }
        }

        public void Play(ESoundType type)
        {
            if (_config == null) return;

            foreach (var entry in _config.Sounds)
            {
                if (entry.Type == type && entry.Clip != null)
                {
                    _sfxSource.PlayOneShot(entry.Clip, entry.Volume);
                    return;
                }
            }
        }
    }
}