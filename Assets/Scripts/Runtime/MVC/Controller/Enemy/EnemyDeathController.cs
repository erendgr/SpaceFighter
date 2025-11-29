using System;
using Runtime.Core.Events;
using Runtime.Core.Factories;
using Runtime.Core.Misc;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using Zenject;

namespace Runtime.MVC.Controller.Enemy
{
    public class EnemyDeathController : IInitializable, IDisposable
    {
        private readonly EnemyView _view;
        private readonly SignalBus _signalBus;
        private readonly AudioPlayer _audioPlayer;
        private readonly EnemyDeathSettings _settings;
        private readonly ExplosionFactory _explosionFactory;

        public EnemyDeathController(SignalBus signalBus, EnemyView view, AudioPlayer audioPlayer,
            EnemySettingsSO settings, ExplosionFactory factory)
        {
            _view = view;
            _signalBus = signalBus;
            _audioPlayer = audioPlayer;
            _settings = settings.DeathSettings;
            _explosionFactory  = factory;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<HitEnemySignal>(OnHitEnemy);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<HitEnemySignal>(OnHitEnemy);
        }

        private void OnHitEnemy(HitEnemySignal signal)
        {
            if (signal.GameObject != _view.gameObject) return;
            
            var explosion = _explosionFactory.Create();
            explosion.transform.position = _view.Position;
            
            _audioPlayer.Play(_settings.DeathSound, _settings.DeathSoundVolume);
            _signalBus.Fire<EnemyDiedSignal>();
            _view.Dispose();
        }
    }
}