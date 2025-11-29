using System;
using Runtime.Core.Events;
using Runtime.Core.Factories;
using Runtime.Core.Misc;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using Zenject;

namespace Runtime.MVC.Controller.Player
{
    public class PlayerHealthController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly AudioPlayer _audioPlayer;
        private readonly PlayerDeathSettings _settings;
        private readonly ExplosionFactory _explosionFactory;
        private PlayerModel _model;
        private PlayerView _view;

        public PlayerHealthController(SignalBus signalBus, AudioPlayer audioPlayer, PlayerModel model,
            PlayerView view, PlayerSettingsSO settings, ExplosionFactory factory)
        {
            _signalBus = signalBus;
            _audioPlayer = audioPlayer;
            _model = model;
            _view = view;
            _settings = settings.DeathSettings;
            _explosionFactory = factory;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<HitPlayerSignal>(OnHitPlayer);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<HitPlayerSignal>(OnHitPlayer);
        }

        private void OnHitPlayer(HitPlayerSignal signal)
        {
            _audioPlayer.Play(_settings.HitSound, _settings.HitSoundVolume);
            _view.AddForce(-signal.HitDirection * _settings.HitForce);
            _model.TakeDamage(_settings.HealthLoss);

            Die();
        }

        private void Die()
        {
            if (_model.Health <= 0 && !_model.IsDead)
            {
                _model.IsDead = true;

                var explosion = _explosionFactory.Create();
                explosion.transform.position = _view.Position;
                _view.Renderer.enabled = false;
                _signalBus.Fire<PlayerDiedSignal>();

                _audioPlayer.Play(_settings.DeathSound, _settings.DeathSoundVolume);
            }
        }
    }
}