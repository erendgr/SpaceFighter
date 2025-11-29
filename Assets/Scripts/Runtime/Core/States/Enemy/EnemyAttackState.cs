using Runtime.Core.Enums;
using Runtime.Core.Factories;
using Runtime.Core.Misc;
using Runtime.MVC.Controller.Enemy;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using UnityEngine;

namespace Runtime.Core.States.Enemy
{
    public class EnemyAttackState : IEnemyState
    {
        private readonly EnemyView _view;
        private readonly EnemyShootSettings _settings;
        private readonly AudioPlayer _audioPlayer;
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly BulletFactory _bulletFactory;
        private readonly EnemyStateController _stateController;
        private readonly EnemyRotationController _rotationController;

        private float _lastShootTime;
        private float _lastStrafeChangeTime;
        private bool _strafeRight;

        public EnemyAttackState(EnemyView view, PlayerModel playerModel, AudioPlayer audioPlayer,
            BulletFactory bulletFactory, PlayerView playerView, EnemySettingsSO settings, 
            EnemyStateController stateController, EnemyRotationController rotationController)
        {
            _view = view;
            _playerModel = playerModel;
            _playerView = playerView;
            _settings = settings.ShootSettings;
            _audioPlayer = audioPlayer;
            _bulletFactory = bulletFactory;
            _stateController = stateController;
            _rotationController = rotationController;
        }

        public void Update()
        {
            if (_playerModel.IsDead)
            {
                _stateController.ChangeState(EnemyStates.Idle);
                return;
            }

            _rotationController.DesiredLookDir = (_playerView.Position - _view.Position).normalized;

            if (Time.realtimeSinceStartup - _lastStrafeChangeTime > _settings.StrafeChangeInterval)
            {
                _lastStrafeChangeTime = Time.realtimeSinceStartup;
                _strafeRight = !_strafeRight;
            }

            if (Time.realtimeSinceStartup - _lastShootTime > _settings.ShootInterval)
            {
                _lastShootTime = Time.realtimeSinceStartup;
                Fire();
            }

            if ((_playerView.Position - _view.Position).magnitude >
                _settings.AttackDistance + _settings.AttackRangeBuffer)
            {
                _stateController.ChangeState(EnemyStates.Follow);
            }
        }

        public void FixedUpdate()
        {
            if (_strafeRight)
            {
                _view.AddForce(_view.RightDir * _settings.StrafeMultiplier);
            }
            else
            {
                _view.AddForce(-_view.RightDir * _settings.StrafeMultiplier);
            }
        }

        private void Fire()
        {
            var bullet = _bulletFactory.Create(_settings.BulletSpeed, _settings.BulletLifetime, BulletTypes.Enemy);

            bullet.transform.position = _view.Position + _view.LookDir * _settings.BulletOffsetDistance;
            bullet.transform.rotation = _view.Rotation;

            _audioPlayer.Play(_settings.ShootSound, _settings.ShootSoundVolume);
        }

        public void ExitState()
        {
        }

        public void EnterState()
        {
        }
    }
}