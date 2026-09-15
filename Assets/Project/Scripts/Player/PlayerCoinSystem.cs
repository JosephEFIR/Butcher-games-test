using Project.Scripts.Common;
using Project.Scripts.Gameplay;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerCoinSystem
    {
        private readonly PlayerModel _model;

        public PlayerCoinSystem(PlayerModel model)
        {
            _model = model;
        }

        public void HandleItem(Item item)
        {
            switch (item.Type)
            {
                case EItemType.Coin:
                    _model.Coins.Value += item.Value;
                    break;

                case EItemType.Alcohol:
                    _model.Coins.Value = Mathf.Max(0, _model.Coins.Value - item.Value);
                    break;

                case EItemType.Gate:
                    break;
            }

            item.Collect();
        }
    }
}