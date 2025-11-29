using Runtime.MVC.Controller.Enemy;
using Runtime.MVC.Model;
using UnityEngine;
using EnemyView = Runtime.MVC.View.EnemyView;

namespace Runtime.Core.States.Enemy
{
    public class EnemyIdleState : IEnemyState
    {
        private readonly EnemyRotationController _rotationController;
        private readonly EnemyDefaultSettings _settings;
        private readonly EnemyView _view;

        private Vector3 _startPos;
        private float _theta;
        private Vector3 _startLookDirection;

        public EnemyIdleState(EnemyView view, EnemySettingsSO settings, EnemyRotationController rotationController)
        {
            _view = view;
            _settings = settings.DefaultSettings;
            _rotationController = rotationController;
        }

        public void EnterState()
        {
            _startPos = _view.Position;
            _theta = Random.Range(0, 2.0f * Mathf.PI);
            _startLookDirection = _view.LookDir;
        }

        public void Update()
        {
            _theta += Time.deltaTime * _settings.Frequency;
            _view.Position = _startPos + _view.RightDir * (_settings.Amplitude * Mathf.Sin(_theta));
            _rotationController.DesiredLookDir = _startLookDirection;
        }

        public void ExitState()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}