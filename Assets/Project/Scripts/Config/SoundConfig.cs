using Project.Scripts.Sound;
using UnityEngine;

namespace Project.Scripts.Config
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Game/Sound Config")]
    public class SoundConfig : ScriptableObject
    {
        [System.Serializable]
        public class SoundEntry
        {
            public ESoundType Type;
            public AudioClip Clip;
            [Range(0f, 1f)] public float Volume = 1f;
        }

        [SerializeField] private AudioClip _musicClip;
        [Range(0f, 1f)] [SerializeField] private float _musicVolume = 0.5f;
        [SerializeField] private SoundEntry[] _sounds;

        public AudioClip MusicClip => _musicClip;
        public float MusicVolume => _musicVolume;
        public SoundEntry[] Sounds => _sounds;
    }
}