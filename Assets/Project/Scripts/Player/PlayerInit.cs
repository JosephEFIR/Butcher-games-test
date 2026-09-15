using System.Collections.Generic;
using Project.Scripts.Animation;
using Project.Scripts.Common;
using Project.Scripts.Config;
using Project.Scripts.Level;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerInit : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerView _view;
        [SerializeField] private HitBoxObserver _hitBox;
        [SerializeField] private Transform[] _waypoints;

        private PlayerModel _model;
        private PlayerController _controller;
        private PlayerCoinSystem _coinSystem;
        private PlayerModelSwipe _modelSwipe;
        private CustomAnimator _animator;
        private InputService _input;

        private void Awake()
        {
            if (_config == null) { Debug.LogError("PlayerConfig not set!"); return; }
            if (_view == null) _view = GetComponent<PlayerView>();
            if (_hitBox == null) _hitBox = GetComponent<HitBoxObserver>();

            var points = new List<Vector3>(_waypoints.Length);
            foreach (var wp in _waypoints) points.Add(wp.position);
            var path = new Path(points);

            _model = new PlayerModel(_config);
            _controller = new PlayerController(_model, _view, _config, path);
            _coinSystem = new PlayerCoinSystem(_model);
            _modelSwipe = new PlayerModelSwipe(_model, _view);
            _animator = new CustomAnimator(_view.Animator, _model);
            _input = new InputService();

            _hitBox.OnItemEntered += _coinSystem.HandleItem;

            Vector3 start = path.GetPoint(0f);
            start.y = 0.3f;
            _view.transform.position = start;
            _view.transform.rotation = Quaternion.LookRotation(path.GetDirection(0f), Vector3.up);
        }

        private void OnDestroy()
        {
            if (_hitBox != null && _coinSystem != null) _hitBox.OnItemEntered -= _coinSystem.HandleItem;
        }

        private void Update()
        {
            if (_controller == null) return;

            _input.Run();
            _controller.HandleInput(_input.Horizontal, Time.deltaTime);
            _controller.Run(Time.deltaTime);

            _modelSwipe.Run();
            _animator.Run();
        }
    }
}