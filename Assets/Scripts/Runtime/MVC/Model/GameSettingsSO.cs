using System;
using UnityEngine;

namespace Runtime.MVC.Model
{
    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "SpaceFighter/Settings/GameSettingsSO", order = 0)]
    public class GameSettingsSO : ScriptableObject
    {
        public GameSettings GameSettings;
    }
    
    [Serializable]
    public class GameSettings
    {
        public float RestartDelay;
    }
}