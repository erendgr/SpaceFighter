using Runtime.Core.Enums;
using Runtime.Core.Factories;
using Runtime.Core.Misc;
using Runtime.Core.States;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.Controller.Player
{
    public class PlayerShootController : ITickable
    {
        private float _lastFireTime;
        
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly PlayerInputStates _inputState;
        private readonly PlayerShootSettings _settings;
        private readonly BulletFactory _bulletFactory;
        private readonly AudioPlayer _audioPlayer;

        public PlayerShootController(PlayerView view, BulletFactory bulletFactory, PlayerSettingsSO settings, 
            PlayerModel model, PlayerInputStates inputState, AudioPlayer audioPlayer)
        {
            _model = model;
            _view = view;
            _inputState = inputState;
            _settings = settings.ShootSettings;
            _audioPlayer = audioPlayer;
            _bulletFactory = bulletFactory;
        }
        
        public void Tick()
        {
            if (_model.IsDead) return;

            if (_inputState.IsFiring && Time.realtimeSinceStartup - _lastFireTime > _settings.MaxShootInterval)
            {
                _lastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }

        private void Fire()
        {
            _audioPlayer.Play(_settings.BulletSound, _settings.BulletSoundVolume);

            var bullet = _bulletFactory.Create(_settings.BulletSpeed, _settings.BulletLifeTime, BulletTypes.Player);
            bullet.transform.position = _view.Position + _view.LookDir * _settings.BulletOffsetDistance;
            bullet.transform.rotation = _view.Rotation;
        }
    }
}