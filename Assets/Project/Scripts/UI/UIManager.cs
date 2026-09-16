using System.Collections.Generic;
using Project.Scripts.Game;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<Board> _boards;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextButton;

        [Inject] private GameInit _gameInit;

        private GameController _controller;

        private void Start()
        {
            _controller = _gameInit.Controller;

            foreach (var board in _boards)
                board.Hide();

            if (_restartButton != null)
                _restartButton.onClick.AddListener(_controller.Restart);

            if (_nextButton != null)
                _nextButton.onClick.AddListener(_controller.NextLevel);

            _controller.OnStateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            if (_controller != null)
                _controller.OnStateChanged -= OnStateChanged;

            if (_restartButton != null) _restartButton.onClick.RemoveAllListeners();
            if (_nextButton != null) _nextButton.onClick.RemoveAllListeners();
        }

        private void OnStateChanged(EGameState state)
        {
            EBoardType? targetType = state switch
            {
                EGameState.Win => EBoardType.Win,
                EGameState.Lose => EBoardType.Lose,
                _ => null
            };

            foreach (var board in _boards)
            {
                if (targetType.HasValue && board.Type == targetType.Value)
                    board.Show();
                else
                    board.Hide();
            }
        }
    }
}