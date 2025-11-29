using System;
using UnityEngine;

namespace Runtime.MVC.Model
{
    [CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "SpaceFighter/Settings/PlayerSettingsSO", order = 0)]
    public class PlayerSettingsSO : ScriptableObject
    {
        public PlayerHealthSettings HealthSettings;
        public PlayerMovementSettings MovementSettings;
        public PlayerShootSettings ShootSettings;
        public PlayerDeathSettings DeathSettings;
    }

    [Serializable]
    public class PlayerHealthSettings
    {
        [Header("Player Health Settings")]
        public float Health;
    }

    [Serializable]
    public class PlayerMovementSettings
    {
        [Header("Player Movement Settings")]
        public float MoveSpeed;
        public float BoundaryBuffer;
        public float BoundaryAdjustForce;
        public float SlowDownSpeed;
    }

    [Serializable]
    public class PlayerShootSettings
    {
        [Header("Player Shoot Settings")] 
        public float BulletLifeTime;
        public float BulletSpeed;
        public float MaxShootInterval;
        public float BulletOffsetDistance;
        
        public AudioClip BulletSound;
        public float BulletSoundVolume = 1.0f;
    }
    
    [Serializable]
    public class PlayerDeathSettings
    {
        [Header("Player Death Settings")] 
        public float HealthLoss;
        public float HitForce;

        public AudioClip DeathSound;
        public float DeathSoundVolume = 1.0f;
        public AudioClip HitSound;
        public float HitSoundVolume = 1.0f;
    }
}