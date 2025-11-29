using System.Collections.Generic;
using Runtime.Core.Enums;
using Runtime.Core.States.Enemy;
using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.Controller.Enemy
{
    public class EnemyStateController : IInitializable, IFixedTickable, ITickable
    {
        private Dictionary<EnemyStates, IEnemyState> _statesMap;
        private IEnemyState _stateHandler;
        private EnemyStates _currentState = EnemyStates.None;
        private EnemyView _view;
        private List<IEnemyState> _states;

        [Inject]
        public void Construct(EnemyView view, EnemyIdleState idle, EnemyAttackState attack, EnemyFollowState follow)
        {
            _view = view;
            
            _statesMap = new Dictionary<EnemyStates, IEnemyState>
            {
                { EnemyStates.Idle, idle },
                { EnemyStates.Attack, attack },
                { EnemyStates.Follow, follow },
            };
        }

        public void Initialize()
        {
            ChangeState(EnemyStates.Follow);
        }

        public void ChangeState(EnemyStates state)
        {
            if (_currentState == state) return;

            _stateHandler?.ExitState();
            _currentState = state;
            _stateHandler = _statesMap[state];
            _stateHandler.EnterState();
        }

        public void FixedTick()
        {
            _stateHandler.FixedUpdate();
        }

        public void Tick()
        {
            _view.Position = new Vector3(_view.Position.x, _view.Position.y, 0);

            _stateHandler.Update();
        }
    }
}