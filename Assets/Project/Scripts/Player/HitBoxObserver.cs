using System;
using Project.Scripts.Gameplay;
using Project.Scripts.Level.Gates;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class HitBoxObserver : MonoBehaviour
    {
        public event Action<Item> OnItemEntered;
        public event Action<Gate> OnGateEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item))
                OnItemEntered?.Invoke(item);

            if (other.TryGetComponent(out Gate gate))
                OnGateEntered?.Invoke(gate);
        }
    }
}