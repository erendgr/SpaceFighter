using System;
using UnityEngine;

namespace Runtime.MVC.Model
{
    [CreateAssetMenu(fileName = "PrefabSettingsSO", menuName = "SpaceFighter/Settings/PrefabSettingsSO", order = 0)]
    public class PrefabSettingsSO : ScriptableObject
    {
        public PrefabSettings PrefabSettings;
    }

    [Serializable]
    public class PrefabSettings
    {
        [Header("Prefab Settings")]
        public GameObject BulletPrefab;
        public GameObject EnemyPrefab;
        public GameObject ExplosionPrefab;
    }
}