using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<SkinEntry> _skins;
        [SerializeField] private ParticleSystem _richnessVfx;

        public Animator Animator => _animator;
        public List<SkinEntry> Skins => _skins;
        public ParticleSystem RichnessVfx => _richnessVfx;
    }
}