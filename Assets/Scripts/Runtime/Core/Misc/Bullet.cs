using Runtime.Core.Enums;
using Runtime.Core.Events;
using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.Core.Misc
{
    public class Bullet : MonoBehaviour, IPoolable<float, float, BulletTypes, IMemoryPool>
    {
        [SerializeField] private MeshRenderer rend;
        [SerializeField] private Material playerMaterial;
        [SerializeField] private Material enemyMaterial;

        private IMemoryPool _pool;
        private float _startTime;
        private BulletTypes _type;
        private float _speed;
        private float _lifeTime;
        [Inject] private SignalBus _signalBus;
        
        public Vector3 _moveDirection
        {
            get { return transform.right; }
        }
        
        private void Update()
        {
            transform.position -= transform.right * _speed * Time.deltaTime;
            
            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                _pool.Despawn(this);
            }
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(float speed, float lifeTime, BulletTypes type, IMemoryPool pool)
        {
            _pool = pool;
            _lifeTime = lifeTime;
            _speed = speed;
            _type = type;

            rend.material = type == BulletTypes.Enemy ? enemyMaterial : playerMaterial;
            _startTime = Time.realtimeSinceStartup;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerView player) && _type == BulletTypes.Enemy)
            {
                _signalBus.Fire(new HitPlayerSignal(other.gameObject, _moveDirection));
                _pool.Despawn(this);
            }
            else if (other.TryGetComponent(out EnemyView enemy) && _type == BulletTypes.Player)
            {
                _signalBus.Fire(new HitEnemySignal(other.gameObject, _moveDirection));
                _pool.Despawn(this);
            }
            
        }
    }
}