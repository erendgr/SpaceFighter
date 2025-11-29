namespace Runtime.Core.States.Enemy
{
    public interface IEnemyState
    {
        public void EnterState();
        public void ExitState();
        public void Update();
        public void FixedUpdate();
    }
}