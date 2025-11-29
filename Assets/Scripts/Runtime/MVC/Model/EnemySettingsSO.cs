using System;
using UnityEngine;

namespace Runtime.MVC.Model
{
    [CreateAssetMenu(fileName = "EnemySettingsSO", menuName = "SpaceFighter/Settings/EnemySettingsSO", order = 0)]
    public class EnemySettingsSO : ScriptableObject
    {
        public EnemySpawnSettings SpawnSettings;
        public EnemyDefaultSettings DefaultSettings;
        public EnemyDeathSettings DeathSettings;
        public EnemyShootSettings ShootSettings;
    }
    
    [Serializable]
    public class EnemyDefaultSettings
    {
        public float Speed;
        public float Accuracy;
        public float TurnSpeed;
        public float Amplitude;
        public float Frequency;
    }

    [Serializable]
    public class EnemySpawnSettings
    {
        public float NumEnemiesIncreaseRate;
        public float NumEnemiesStartAmount;
        public float MinDelayBetweenSpawns = 0.5f;
    }
    
    [Serializable]
    public class EnemyDeathSettings
    {
        public AudioClip DeathSound;
        public float DeathSoundVolume;
    }
    
    [Serializable]
    public class EnemyShootSettings
    {
        public AudioClip ShootSound;
        public float ShootSoundVolume = 1.0f;

        public float BulletLifetime;
        public float BulletSpeed;
        public float BulletOffsetDistance;
        public float ShootInterval;
        public float AttackRangeBuffer;
        public float StrafeMultiplier;
        public float StrafeChangeInterval;
        public float TeleportNewDistance;
        public float AttackDistance = 15.0f;
    }
}