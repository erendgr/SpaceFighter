using UnityEngine;
using Zenject;

namespace Runtime.MVC.Model
{
    public class PlayerModel
    {
        public float Health { get; private set; }
        public float MovementSpeed { get; }
        public bool IsDead { get; set; }

        [Inject]
        public PlayerModel(PlayerSettingsSO settings)
        {
            Health = settings.HealthSettings.Health;
            MovementSpeed = settings.MovementSettings.MoveSpeed;
        }

        public void TakeDamage(float amount)
        {
            Health = Mathf.Max(0, Health - amount);
        }
    }
}