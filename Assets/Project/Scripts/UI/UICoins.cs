using Project.Scripts.Player;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Project.Scripts.UI
{
    public class UICoins : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;

        [Inject] private PlayerInit _player;
        private CompositeDisposable _disposable = new();
        
        private void Start()
        {
            _player.Model.Coins.Subscribe(coins => _coinsText.text = ((int)coins).ToString()).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
            _disposable.Clear();
        }
    }
}