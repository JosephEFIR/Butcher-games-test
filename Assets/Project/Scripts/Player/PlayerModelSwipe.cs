using System.Collections.Generic;

namespace Project.Scripts.Player
{
    public class PlayerModelSwipe
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        private readonly List<SkinEntry> _sorted;
        private int _currentIndex = -1;

        public PlayerModelSwipe(PlayerModel model, PlayerView view)
        {
            _model = model;
            _view = view;

            _sorted = new List<SkinEntry>(_view.Skins);
            _sorted.Sort((a, b) => a.Threshold.CompareTo(b.Threshold));
            
            for (int i = 0; i < _sorted.Count; i++)
            {
                var s = _sorted[i];
            }

            ApplyForRichness(_model.Coins.Value);
        }

        public void Run()
        {
            ApplyForRichness(_model.Coins.Value);
        }

        private void ApplyForRichness(float richness)
        {
            int targetIndex = -1;

            for (int i = 0; i < _sorted.Count; i++)
            {
                if (richness >= _sorted[i].Threshold)
                    targetIndex = i;
                else
                    break;
            }

            if (targetIndex == _currentIndex) return;

            bool gotRicher = targetIndex > _currentIndex;

            _currentIndex = targetIndex;

            for (int i = 0; i < _sorted.Count; i++)
            {
                if (_sorted[i].Model != null)
                {
                    bool active = i == _currentIndex;
                    _sorted[i].Model.SetActive(active);
                }
            }

            if (gotRicher && _view.PositiveVFX != null) _view.PositiveVFX.Play();
        }
    }
}