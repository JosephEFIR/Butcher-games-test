using System.Collections.Generic;
using Project.Scripts.Player;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI
{
    public class UIRichnessBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _tierNameText;

        [Inject] private PlayerInit _player;

        private List<SkinEntry> _sorted;
        private CompositeDisposable _disposable = new();

        private void Start()
        {
            _sorted = new List<SkinEntry>(_player.View.Skins);
            _sorted.Sort((a, b) => a.Threshold.CompareTo(b.Threshold));

            _player.Model.Coins.Subscribe(OnCoinsChanged).AddTo(_disposable);
        }

        private void OnCoinsChanged(float coins)
        {
            if (_sorted == null || _sorted.Count == 0) return;
            
            int currentTier = 0;
            for (int i = 0; i < _sorted.Count; i++)
            {
                if (coins >= _sorted[i].Threshold) currentTier = i;
                else break;
            }

            int nextTier = currentTier + 1;

            float currentThreshold = _sorted[currentTier].Threshold;
            _tierNameText.text = _sorted[currentTier].DisplayName;

            if (nextTier < _sorted.Count)
            {
                float nextThreshold = _sorted[nextTier].Threshold;

                _slider.minValue = currentThreshold;
                _slider.maxValue = nextThreshold;
                
                _slider.value = Mathf.Clamp(coins, currentThreshold, nextThreshold);
            }
            else
            {
                _slider.minValue = currentThreshold;
                _slider.maxValue = currentThreshold + 1f;
                _slider.value = currentThreshold + 1f;
            }
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}