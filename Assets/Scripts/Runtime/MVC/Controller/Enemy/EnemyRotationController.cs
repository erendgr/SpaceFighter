using Runtime.MVC.Model;
using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.Controller.Enemy
{
    public class EnemyRotationController : IFixedTickable
    {
        private readonly EnemyDefaultSettings _settings;
        private readonly EnemyView _view;
        
        public Vector2 DesiredLookDir
        {
            get; set;
        }

        public EnemyRotationController(EnemyView view, EnemySettingsSO settings)
        {
            _view = view;
            _settings = settings.DefaultSettings;
        }
        
        public void FixedTick()
        {
            var targetRotation = Quaternion.LookRotation(DesiredLookDir) * Quaternion.AngleAxis(90, Vector3.up);
            
            _view.Rotation = Quaternion.Slerp(_view.Rotation, targetRotation, _settings.TurnSpeed * Time.fixedDeltaTime);
        }
    }
}