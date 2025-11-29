using ModestTree;
using Runtime.Core.Events;
using Runtime.Core.Factories;
using Runtime.Core.Misc;
using Runtime.MVC.Model;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.Controller.Enemy
{
    public class EnemySpawnController : ITickable, IInitializable
    {
        private readonly EnemyFactory _factory;
        private readonly SignalBus _signalBus;
        private readonly LevelBoundary _levelBoundary;
        private readonly EnemySpawnSettings _settings;
        
        private float _maxEnemyCount;
        private int _enemyCount;
        private float _lastSpawnTime;
        
        public EnemySpawnController(SignalBus signalBus, LevelBoundary levelBoundary,
            EnemySettingsSO settings, EnemyFactory factory)
        {
            _factory = factory;
            _signalBus = signalBus;
            _settings = settings.SpawnSettings;
            _levelBoundary = levelBoundary;

            _maxEnemyCount = settings.SpawnSettings.NumEnemiesStartAmount;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
        }

        private void OnEnemyDied()
        {
            _enemyCount--;
        }

        public void Tick()
        {
            _maxEnemyCount += _settings.NumEnemiesIncreaseRate * Time.deltaTime;

            if (_enemyCount < (int)_maxEnemyCount 
                && Time.realtimeSinceStartup - _lastSpawnTime > _settings.MinDelayBetweenSpawns)
            {
                SpawnEnemy();
                _enemyCount++;
            }
        }

        private void SpawnEnemy()
        {
            var enemyView = _factory.Create();
            enemyView.Position = ChooseRandomStartPosition();

            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        private Vector3 ChooseRandomStartPosition()
        {
            var side = Random.Range(0, 4);
            var posOnSide = Random.Range(0, 1.0f);

            float buffer = 2.0f;

            switch (side)
            {
                case 0:/*Top*/
                {
                    return new Vector3(_levelBoundary.Left + posOnSide * _levelBoundary.Width,
                        _levelBoundary.Top + buffer, 0);
                }
                case 1:/*Right*/
                {
                    return new Vector3(_levelBoundary.Right + buffer,
                        _levelBoundary.Top - posOnSide * _levelBoundary.Height, 0);
                }
                case 2:/*Bottom*/
                {
                    return new Vector3(_levelBoundary.Left + posOnSide * _levelBoundary.Width,
                        _levelBoundary.Bottom - buffer, 0);
                }
                case 3:/*Left*/
                {
                    return new Vector3(_levelBoundary.Left - buffer, _levelBoundary.Top - posOnSide * 
                        _levelBoundary.Height, 0);
                }
            }

            throw Assert.CreateException();
        }
    }
}