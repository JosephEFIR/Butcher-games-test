using UnityEngine;

namespace Project.Scripts.Common
{
    public class ItemSpinner : MonoBehaviour
    {
        [Header("Rotation")]
        [SerializeField] private float _rotationSpeed = 30f;
        [SerializeField] private Vector3 _axis = Vector3.up;

        [Header("Float")]
        [SerializeField] private bool _floatEnabled = true;
        [SerializeField] private float _floatAmplitude = 0.15f;
        [SerializeField] private float _floatSpeed = 1.5f;

        private Vector3 _startPos;

        private void Start()
        {
            _startPos = transform.localPosition;
        }

        private void Update()
        {
            transform.Rotate(_axis * _rotationSpeed * Time.deltaTime, Space.Self);

            if (_floatEnabled)
            {
                float y = Mathf.Sin(Time.time * _floatSpeed) * _floatAmplitude;
                transform.localPosition = _startPos + Vector3.up * y;
            }
        }
    }
}