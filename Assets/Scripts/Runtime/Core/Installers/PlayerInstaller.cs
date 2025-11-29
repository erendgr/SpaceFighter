using Runtime.Core.States;
using Runtime.MVC.Controller.Player;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private PlayerSettingsSO playerSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(playerSettings).IfNotBound();
            Container.Bind<PlayerView>().FromComponentInHierarchy(playerPrefab).AsSingle().NonLazy();
            Container.Bind<PlayerModel>().AsSingle().WithArguments(playerSettings);
            Container.Bind<PlayerInputStates>().AsSingle();
            Container.BindInterfacesTo<PlayerInputController>().AsSingle();
            Container.BindInterfacesTo<PlayerMovementController>().AsSingle();
            Container.BindInterfacesTo<PlayerRotateController>().AsSingle();
            Container.BindInterfacesTo<PlayerShootController>().AsSingle();
            Container.BindInterfacesTo<PlayerHealthController>().AsSingle();
        }
    }
}