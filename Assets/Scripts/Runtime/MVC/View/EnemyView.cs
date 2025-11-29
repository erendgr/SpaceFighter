using System;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.View
{
    public class EnemyView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private MeshRenderer rend;

        private IMemoryPool _pool;
        
        public Vector3 Position
        {
            get => rb.position;
            set => rb.position = value;
        }
        
        public Quaternion Rotation
        {
            get { return rb.rotation; }
            set { rb.rotation = value; }
        }
        
        public Vector3 LookDir
        {
            get { return -rb.transform.right; }
        }

        public Vector3 RightDir
        {
            get { return rb.transform.up; }
        }
        
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }
        
        public void AddForce(Vector3 force)
        {
            rb.AddForce(force);
        }
    }
}