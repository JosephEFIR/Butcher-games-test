using Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Common
{
    public class LookAtPlayer : MonoBehaviour
    {
        [Inject] private PlayerInit _player;

        private void LateUpdate()
        {
            if (_player == null) return;

            Vector3 target = _player.transform.position;

            target.y = transform.position.y;
            Vector3 dir = target - transform.position;
            if (dir.sqrMagnitude < 0.0001f) return;

            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}