using Runtime.Core.Enums;
using Runtime.Core.Factories;
using Runtime.Core.Misc;
using Runtime.Core.Pools;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.Core.Installers
{
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private PrefabSettingsSO prefabSettings;
        [SerializeField] private EnemySettingsSO enemySettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(enemySettings).IfNotBound();
            
            Container.BindFactory<float, float, BulletTypes, Bullet, BulletFactory>()
                .FromPoolableMemoryPool<float, float, BulletTypes, Bullet, BulletPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(prefabSettings.PrefabSettings.BulletPrefab)
                    .UnderTransformGroup("Bullets"));
            Container.BindFactory<Explosion, ExplosionFactory>()
                .FromPoolableMemoryPool<Explosion, ExplosionPool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(prefabSettings.PrefabSettings.ExplosionPrefab)
                    .UnderTransformGroup("Explosion"));
            Container.BindFactory<EnemyView, EnemyFactory>()
                .FromPoolableMemoryPool<EnemyView, EnemyPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<EnemyInstaller>(prefabSettings.PrefabSettings.EnemyPrefab)
                    .UnderTransformGroup("Enemies"));
        }
    }
}