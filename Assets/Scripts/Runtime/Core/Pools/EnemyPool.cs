using Runtime.MVC.View;
using Zenject;

namespace Runtime.Core.Pools
{
    public class EnemyPool: MonoPoolableMemoryPool<IMemoryPool, EnemyView>
    {
        
    }
}