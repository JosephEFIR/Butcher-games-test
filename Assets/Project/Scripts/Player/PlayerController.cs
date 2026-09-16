using Project.Scripts.Config;
using Project.Scripts.Level;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerController
    {
        private const float PlayerY = 0.3f;

        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly PlayerConfig _config;
        private readonly Path _path;

        private float _distance;
        private float _desiredX;
        private float _currentX;
        private Vector3 _smoothForward = Vector3.forward;

        public PlayerController(PlayerModel model, PlayerView view, PlayerConfig config, Path path)
        {
            _model = model;
            _view = view;
            _config = config;
            _path = path;

            _smoothForward = _path.GetDirection(0f);
        }

        public void Run(float deltaTime)
        {
            if (!_model.IsAlive || _model.IsFinished) return;
            
            Vector3 rawForward = _path.GetDirection(_distance);
            _smoothForward = Vector3.Slerp(
                _smoothForward, rawForward, deltaTime * _config.TurnSmooth).normalized;
            Vector3 forward = _smoothForward;
            
            _currentX = Mathf.MoveTowards(_currentX, _desiredX, _config.LateralSmooth * deltaTime);

            Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
            _currentX = ClampXByWalls(_path.GetPoint(_distance), right, _currentX);
            
            Vector3 checkOrigin = _view.transform.position + Vector3.up * 0.5f;
            bool blocked = Physics.Raycast(
                checkOrigin,
                forward,
                _config.ObstacleCheckDistance,
                _config.ObstacleLayer);
            
            if (!blocked)
                _distance += _config.ForwardSpeed * deltaTime;
            
            Vector3 pathPoint = _path.GetPoint(_distance);
            pathPoint.y = PlayerY;
            Vector3 targetPos = pathPoint + right * _currentX;

            Vector3 currentPos = _view.transform.position;
            Vector3 newPos = Vector3.Lerp(currentPos, targetPos, deltaTime * 10f);

            Vector3 moveDelta = newPos - currentPos;
            float maxMove = (_config.ForwardSpeed + _config.LateralSpeed) * deltaTime * 1.5f;
            if (moveDelta.magnitude > maxMove)
                newPos = currentPos + moveDelta.normalized * maxMove;

            newPos.y = PlayerY;
            _view.transform.position = newPos;

            if (forward.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(forward, Vector3.up);
                _view.transform.rotation = Quaternion.Slerp(
                    _view.transform.rotation, targetRot, deltaTime * _config.TurnSmooth);
            }

            if (_path.IsFinished(_distance))
                _model.IsFinished = true;
        }

        public void HandleInput(float horizontal, float deltaTime)
        {
            if (!_model.IsAlive || _model.IsFinished) return;

            _desiredX += horizontal * _config.LateralSpeed * deltaTime;
            _desiredX = Mathf.Clamp(_desiredX, -_config.XLimit, _config.XLimit);
        }

        private float ClampXByWalls(Vector3 origin, Vector3 right, float desiredX)
        {
            Vector3 checkOrigin = origin + Vector3.up * 1f;

            float maxRight = _config.WallCheckDistance;
            float maxLeft = _config.WallCheckDistance;

            if (Physics.Raycast(checkOrigin, right, out var hitR,
                    _config.WallCheckDistance, _config.WallLayer))
                maxRight = hitR.distance - _config.PlayerRadius;

            if (Physics.Raycast(checkOrigin, -right, out var hitL,
                    _config.WallCheckDistance, _config.WallLayer))
                maxLeft = hitL.distance - _config.PlayerRadius;

            float limit = Mathf.Min(maxLeft, maxRight, _config.XLimit);
            return Mathf.Clamp(desiredX, -limit, limit);
        }
    }
}