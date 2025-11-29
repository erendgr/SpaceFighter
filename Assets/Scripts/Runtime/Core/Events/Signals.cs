using UnityEngine;

namespace Runtime.Core.Events
{
    public struct HitSignal
    {
        public readonly Collider Collider;
        public HitSignal(Collider collider) => Collider = collider;
    }
    
    public struct HitPlayerSignal
    {
        public readonly GameObject GameObject;
        public readonly Vector3 HitDirection;
        public HitPlayerSignal(GameObject gameObject, Vector3 hitDirection) 
        {
            GameObject = gameObject;
            HitDirection = hitDirection;
        }
    }
    
    public struct HitEnemySignal
    {
        public readonly GameObject GameObject;
        public readonly Vector3 HitDirection;
        public HitEnemySignal(GameObject gameObject, Vector3 hitDirection) 
        {
            GameObject = gameObject;
            HitDirection = hitDirection;
        }
    }
    
    public struct PlayerDiedSignal { }

    public struct EnemyDiedSignal { }
    
    public struct EnemyKilledSignal { }
}