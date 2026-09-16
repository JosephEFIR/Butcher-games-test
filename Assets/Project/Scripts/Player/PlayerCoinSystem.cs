using Project.Scripts.Common;
using Project.Scripts.Gameplay;
using Project.Scripts.Level.Gates;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerCoinSystem
    {
        private readonly PlayerModel _model;
        private readonly PlayerVFX _vfx;

        public PlayerCoinSystem(PlayerModel model, PlayerVFX vfx)
        {
            _model = model;
            _vfx = vfx;
        }

        public void HandleItem(Item item)
        {
            switch (item.Type)
            {
                case EItemType.Coin:
                    if (item.Value >= 0) _vfx.PlayPositive();
                    else _vfx.PlayNegative();

                    _model.Coins.Value += item.Value;
                    if (_model.Coins.Value < 0) _model.Coins.Value = 0;
                    break;

                case EItemType.Alcohol:
                    if (item.Value >= 0) _vfx.PlayNegative();
                    else _vfx.PlayNegative();

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
                    _model.Coins.Value += gate.Value;
                    break;

                case EGateType.Negative:
                    _vfx.PlayNegative();
                    _model.Coins.Value = Mathf.Max(0, _model.Coins.Value - gate.Value);
                    break;
            }
        }
    }
}