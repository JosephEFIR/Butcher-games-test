using Project.Scripts.Config;
using Project.Scripts.Player;
using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();

            Container.Bind<GameInit>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInit>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UICoins>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIRichnessBar>().FromComponentInHierarchy().AsSingle();
        }
    }
}