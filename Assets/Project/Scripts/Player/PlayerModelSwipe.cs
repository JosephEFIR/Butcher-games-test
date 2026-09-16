using System.Collections.Generic;
using Project.Scripts.Audio;
using Project.Scripts.Sound;

namespace Project.Scripts.Player
{
    public class PlayerModelSwipe
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly SoundService _sound;

        private readonly List<SkinEntry> _sorted;
        private int _currentIndex = -1;

        public PlayerModelSwipe(PlayerModel model, PlayerView view, SoundService sound)
        {
            _model = model;
            _view = view;
            _sound = sound;

            _sorted = new List<SkinEntry>(_view.Skins);
            _sorted.Sort((a, b) => a.Threshold.CompareTo(b.Threshold));
            
            _currentIndex = FindTierIndex(_model.Coins.Value);
            ApplySkinVisual(_currentIndex);
        }

        public void Run()
        {
            ApplyForRichness(_model.Coins.Value);
        }

        private void ApplyForRichness(float coins)
        {
            int targetIndex = FindTierIndex(coins);

            if (targetIndex == _currentIndex) return;

            bool gotRicher = targetIndex > _currentIndex;
            _currentIndex = targetIndex;

            ApplySkinVisual(_currentIndex);

            if (gotRicher)
            {
                if (_sound != null)
                    _sound.Play(ESoundType.SkinUpgrade);

                if (_view.PositiveVFX != null)
                    _view.PositiveVFX.Play();
            }
        }

        private int FindTierIndex(float coins)
        {
            int target = -1;
            for (int i = 0; i < _sorted.Count; i++)
            {
                if (coins >= _sorted[i].Threshold)
                    target = i;
                else
                    break;
            }
            return target;
        }

        private void ApplySkinVisual(int index)
        {
            for (int i = 0; i < _sorted.Count; i++)
            {
                if (_sorted[i].Model != null)
                    _sorted[i].Model.SetActive(i == index);
            }
        }
    }
}