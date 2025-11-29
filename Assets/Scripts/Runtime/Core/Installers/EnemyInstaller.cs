using Runtime.Core.States.Enemy;
using Runtime.MVC.Controller.Enemy;
using Zenject;

namespace Runtime.Core.Installers
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyStateController>().AsSingle();
            Container.Bind<EnemyIdleState>().AsSingle();
            Container.Bind<EnemyAttackState>().AsSingle();
            Container.Bind<EnemyFollowState>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyDeathController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRotationController>().AsSingle();
        }
    }
}