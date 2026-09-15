using Project.Scripts.Common;
using UnityEngine;

namespace Project.Scripts.Gameplay
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private EItemType _type;
        [SerializeField] private int _value = 1;

        public EItemType Type => _type;
        public int Value => _value;

        public void Collect()
        {
            // Тут в будущем — VFX, звук.
            Destroy(gameObject);
        }
    }
}