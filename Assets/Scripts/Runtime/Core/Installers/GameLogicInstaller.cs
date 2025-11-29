using Runtime.Core.Misc;
using Runtime.MVC.Controller.Enemy;
using Runtime.MVC.Model;
using UnityEngine;
using Zenject;

namespace Runtime.Core.Installers
{
    public class GameLogicInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameSettingsSO gameSettingsSO;

        public override void InstallBindings()
        {
            Container.BindInstance(gameSettingsSO).IfNotBound();
            Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
            Container.Bind<LevelBoundary>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnController>().AsSingle();
            Container.BindInterfacesTo<GameRestartManager>().AsSingle();
        }
    }
}