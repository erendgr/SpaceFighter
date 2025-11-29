using System;
using Runtime.Core.Events;
using Runtime.MVC.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Runtime.Core.Misc
{
    public class GameRestartManager : IInitializable, IDisposable, ITickable
    {
        private readonly SignalBus _signalBus;
        private readonly GameSettings _settings;
        
        private bool _isDelaying;
        private float _delayStartTime;

        public GameRestartManager(SignalBus signalBus, GameSettingsSO settings)
        {
            _signalBus = signalBus;
            _settings = settings.GameSettings;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }
        
        private void OnPlayerDied(PlayerDiedSignal signal)
        {
            _delayStartTime = Time.realtimeSinceStartup;
            _isDelaying = true;
        }

        public void Tick()
        {
            if (!_isDelaying) 
                return;

            if (Time.realtimeSinceStartup - _delayStartTime >= _settings.RestartDelay)
            {
                _isDelaying = false;

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        // private async void OnPlayerDied(PlayerDiedSignal signal)
        // {
        //     float delay = _gameSettings.RestartDelay;
        //
        //     await Task.Delay((int)(delay * 1000));
        //
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }
    }
}