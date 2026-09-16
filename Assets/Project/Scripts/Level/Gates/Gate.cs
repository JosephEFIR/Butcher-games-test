using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Level.Gates
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private EGateType type = EGateType.Positive;
        [SerializeField] private int _value = 100;

        public EGateType Type => type;
        public int Value => _value;

        private bool _used;

        public bool TryUse()
        {
            if (_used) return false;
            _used = true;
            return true;
        }
    }
}