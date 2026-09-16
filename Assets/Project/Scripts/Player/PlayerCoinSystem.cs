using Project.Scripts.Audio;
using Project.Scripts.Common;
using Project.Scripts.Gameplay;
using Project.Scripts.Level.Gates;
using Project.Scripts.Sound;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerCoinSystem
    {
        private readonly PlayerModel _model;
        private readonly PlayerVFX _vfx;
        private readonly SoundService _sound;

        public PlayerCoinSystem(PlayerModel model, PlayerVFX vfx, SoundService sound)
        {
            _model = model;
            _vfx = vfx;
            _sound = sound;
        }

        public void HandleItem(Item item)
        {
            switch (item.Type)
            {
                case EItemType.Coin:
                    _vfx.PlayPositive();
                    _sound.Play(ESoundType.CoinPickup);
                    _model.Coins.Value += item.Value;
                    if (_model.Coins.Value < 0) _model.Coins.Value = 0;
                    break;

                case EItemType.Alcohol:
                    _vfx.PlayNegative();
                    _sound.Play(ESoundType.GateNegative);
                    _model.Coins.Value = Mathf.Max(0, _model.Coins.Value - item.Value);
                    break;
            }

            item.Collect();
        }

        public void HandleGate(Gate gate)
        {
            if (!gate.TryUse()) return;
            switch (gate.Type)
            {
                case EGateType.Positive:
                    _vfx.PlayPositive();
                    _sound.Play(ESoundType.GatePositive);
                    _model.Coins.Value += gate.Value;
                    break;

                case EGateType.Negative:
                    _vfx.PlayNegative();
                    _sound.Play(ESoundType.GateNegative);
                    _model.Coins.Value = Mathf.Max(0, _model.Coins.Value - gate.Value);
                    break;
            }
        }
    }
}