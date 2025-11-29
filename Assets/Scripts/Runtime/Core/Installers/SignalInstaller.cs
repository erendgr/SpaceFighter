using Runtime.Core.Events;
using Zenject;

namespace Runtime.Core.Installers
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<HitSignal>();
            Container.DeclareSignal<HitPlayerSignal>();
            Container.DeclareSignal<HitEnemySignal>();
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<EnemyDiedSignal>();
            Container.DeclareSignal<EnemyKilledSignal>();
        }
    }
}