using Runtime.Core.States;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.Controller.Player
{
    public class PlayerInputController : ITickable
    {
        readonly PlayerInputStates _inputStates;

        public PlayerInputController(PlayerInputStates inputStates)
        {
            _inputStates = inputStates;
        }
        
        public void Tick()
        {
            _inputStates.IsMovingDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
            _inputStates.IsMovingUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
            _inputStates.IsMovingLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
            _inputStates.IsMovingRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            _inputStates.IsFiring = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
        }
    }
}