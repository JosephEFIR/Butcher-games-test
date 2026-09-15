using Project.Scripts.Common;
using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Animation
{
    public class CustomAnimator
    {
        private readonly Animator _animator;
        private readonly PlayerModel _model;

        private bool _lastAlive;
        private bool _lastFinished;

        public CustomAnimator(Animator animator, PlayerModel model)
        {
            _animator = animator;
            _model = model;

            _lastAlive = _model.IsAlive;
            _lastFinished = _model.IsFinished;
        }

        public void Run()
        {
            if (_animator == null) return;

            if (_model.IsAlive != _lastAlive)
            {
                _lastAlive = _model.IsAlive;
                if (!_lastAlive)
                    _animator.SetTrigger(EAnimType.Die);
            }

            if (_model.IsFinished != _lastFinished)
            {
                _lastFinished = _model.IsFinished;
                if (_lastFinished)
                    _animator.SetTrigger(EAnimType.Win);
            }
        }
    }
}