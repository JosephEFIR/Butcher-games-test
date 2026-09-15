using System;
using Project.Scripts.Gameplay;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class HitBoxObserver : MonoBehaviour
    {
        public event Action<Item> OnItemEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                OnItemEntered?.Invoke(item);
            }
        }
    }
}