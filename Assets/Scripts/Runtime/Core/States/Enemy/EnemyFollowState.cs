using Runtime.Core.Enums;
using Runtime.MVC.Controller.Enemy;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using UnityEngine;

namespace Runtime.Core.States.Enemy
{
    public class EnemyFollowState : IEnemyState
    {
        private readonly EnemyRotationController _rotationController;
        private readonly EnemyDefaultSettings _defaultSettings;
        private readonly EnemyShootSettings _shootSettings;
        private readonly EnemyStateController _stateController;
        private readonly EnemyView _view;
        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;

        private bool _strafeRight;
        private float _lastStrafeChangeTime;
        
        public EnemyFollowState(PlayerView playerView, EnemyView view, EnemyStateController stateController,
            EnemySettingsSO settings, PlayerModel playerModel, EnemyRotationController rotationController)
        {
            _view = view;
            _playerView = playerView;
            _playerModel = playerModel;
            _stateController = stateController;
            _shootSettings = settings.ShootSettings;
            _rotationController = rotationController;
            _defaultSettings = settings.DefaultSettings;
        }
        
        public void EnterState()
        {
            _strafeRight = Random.Range(0, 1) == 0;
            _lastStrafeChangeTime = Time.realtimeSinceStartup;
        }

        public void Update()
        {
            if (_playerModel.IsDead)
            {
                _stateController.ChangeState(EnemyStates.Idle);
                return;
            }

            var distanceToPlayer = (_playerView.Position - _view.Position).magnitude;
            
            _rotationController.DesiredLookDir = (_playerView.Position - _view.Position).normalized;

            if (Time.realtimeSinceStartup - _lastStrafeChangeTime > _shootSettings.StrafeChangeInterval)
            {
                _lastStrafeChangeTime = Time.realtimeSinceStartup;
                _strafeRight = !_strafeRight;
            }

            if (distanceToPlayer < _shootSettings.AttackDistance)
            {
                _stateController.ChangeState(EnemyStates.Attack);
            }
        }

        public void FixedUpdate()
        {
            MoveTowardsPlayer();
            Strafe();
        }

        private void Strafe()
        {
            if (_strafeRight)
            {
                _view.AddForce(_view.RightDir * _shootSettings.StrafeMultiplier * _defaultSettings.Speed);
            }
            else
            {
                _view.AddForce(-_view.RightDir * _shootSettings.StrafeMultiplier * _defaultSettings.Speed);
            }
        }

        private void MoveTowardsPlayer()
        {
            var playerDir = (_playerView.Position - _view.Position).normalized;
            _view.AddForce(playerDir * _defaultSettings.Speed);
        }

        public void ExitState() { }
    }
}