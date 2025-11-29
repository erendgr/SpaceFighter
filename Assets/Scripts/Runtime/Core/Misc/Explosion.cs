using UnityEngine;
using Zenject;

namespace Runtime.Core.Misc
{
    public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private ParticleSystem particleSystem;

        private float _startTime;
        private IMemoryPool _pool;

        private void Update()
        {
            if (Time.realtimeSinceStartup - _startTime > lifeTime)
            {
                _pool.Despawn(this);
            }
        }

        public void OnDespawned()
        {
        }

        public void OnSpawned(IMemoryPool pool)
        {
            particleSystem.Clear();
            particleSystem.Play();

            _startTime = Time.realtimeSinceStartup;
            _pool = pool;
        }
    }
}