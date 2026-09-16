using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Game
{
    [DefaultExecutionOrder(100)]
    public class GameInit : MonoBehaviour
    {
        [SerializeField] private PlayerInit _player;
        [SerializeField] private int _winThreshold = 200;

        private GameModel _model;
        private GameController _controller;
        public GameController Controller => _controller;

        private void Awake()
        {
            _model = new GameModel();

            var finishSystem = new FinishSystem(_player.Model, _winThreshold);
            _controller = new GameController(_model, finishSystem);

            _player.SetGameModel(_model);
        }

        private void Start()
        {
            _controller.StartGame();
        }

        private void Update()
        {
            if (_controller == null) return;
            if (_model.CurrentState != EGameState.Gameplay) return;

            if (_player.Model.IsFinished)
                _controller.OnFinishReached();
            else if (!_player.Model.IsAlive)
                _controller.Lose();
        }
    }
}