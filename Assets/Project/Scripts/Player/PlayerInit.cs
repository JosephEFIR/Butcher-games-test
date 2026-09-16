using System.Collections.Generic;
using Project.Scripts.Animation;
using Project.Scripts.Audio;
using Project.Scripts.Common;
using Project.Scripts.Config;
using Project.Scripts.Game;
using Project.Scripts.Level;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerInit : MonoBehaviour
    {
        [SerializeField] private PlayerView _view;
        [SerializeField] private HitBoxObserver _hitBox;
        [SerializeField] private Transform[] _waypoints;

        [Inject] private PlayerConfig _config;
        [Inject] private SoundService _sound;

        private PlayerModel _model;
        private PlayerController _controller;
        private PlayerCoinSystem _coinSystem;
        private PlayerModelSwipe _modelSwipe;
        private CustomAnimator _animator;
        private InputService _input;
        private GameModel _gameModel;

        public PlayerView View => _view;
        public PlayerModel Model => _model;

        public void SetGameModel(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        private void Awake()
        {
            if (_config == null) { Debug.LogError("PlayerConfig not found!"); return; }
            if (_view == null) _view = GetComponent<PlayerView>();
            if (_hitBox == null) _hitBox = GetComponent<HitBoxObserver>();

            var points = new List<Vector3>(_waypoints.Length);
            foreach (var wp in _waypoints) points.Add(wp.position);
            var path = new Path(points);

            _model = new PlayerModel(_config);
            _controller = new PlayerController(_model, _view, _config, path);

            var vfx = new PlayerVFX(_view);
            _coinSystem = new PlayerCoinSystem(_model, vfx, _sound);
            _modelSwipe = new PlayerModelSwipe(_model, _view, _sound);
            _animator = new CustomAnimator(_view.Animator, _model);
            _input = new InputService();

            _hitBox.OnItemEntered += _coinSystem.HandleItem;
            _hitBox.OnGateEntered += _coinSystem.HandleGate;

            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            Vector3 start = path.GetPoint(0f);
            start.y = 0.3f;
            _view.transform.position = start;
            _view.transform.rotation = Quaternion.LookRotation(path.GetDirection(0f), Vector3.up);
        }

        private void OnDestroy()
        {
            if (_hitBox == null || _coinSystem == null) return;

            _hitBox.OnItemEntered -= _coinSystem.HandleItem;
            _hitBox.OnGateEntered -= _coinSystem.HandleGate;
        }

        private void Update()
        {
            if (_controller == null) return;
            if (_gameModel == null) return;
            if (_gameModel.CurrentState != EGameState.Gameplay) return;

            _input.Run();
            _controller.HandleInput(_input.Horizontal, Time.deltaTime);
            _controller.Run(Time.deltaTime);

            _modelSwipe.Run();
            _animator.Run();
        }
    }
}