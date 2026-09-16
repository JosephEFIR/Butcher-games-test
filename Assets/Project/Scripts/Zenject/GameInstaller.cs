using Project.Scripts.Audio;
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
        [SerializeField] private SoundConfig _soundConfig;

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            Container.Bind<SoundConfig>().FromInstance(_soundConfig).AsSingle();
            
            Container.Bind<GameInit>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInit>().FromComponentInHierarchy().AsSingle();
            Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioInit>().FromComponentInHierarchy().AsSingle();
            
            Container.Bind<SoundService>().FromMethod(ctx => ctx.Container.Resolve<AudioInit>().Service).AsSingle();
        }
    }
}