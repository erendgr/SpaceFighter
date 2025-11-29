using Runtime.Core.Misc;
using Runtime.Core.States;
using Runtime.MVC.Model;
using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.Controller.Player
{
    public class PlayerMovementController : IFixedTickable
    {
        private readonly LevelBoundary _levelBoundary;
        private readonly PlayerMovementSettings _settings;
        private readonly PlayerView _playerView;
        private readonly PlayerModel _playerModel;
        private readonly PlayerInputStates _playerInputState;

        public PlayerMovementController(PlayerView playerView, LevelBoundary levelBoundary,
        PlayerSettingsSO playerSettingsSo, PlayerModel playerModel, PlayerInputStates playerInputState)
        {
            _playerInputState = playerInputState;
            _playerView = playerView;
            _settings = playerSettingsSo.MovementSettings;
            _levelBoundary = levelBoundary;
            _playerModel = playerModel;
        }

        public void FixedTick()
        {
            if (_playerModel.IsDead) return;

            Vector3 dir = Vector3.zero;

            if (_playerInputState.IsMovingLeft) dir += Vector3.left;
            if (_playerInputState.IsMovingRight) dir += Vector3.right;
            if (_playerInputState.IsMovingUp) dir += Vector3.up;
            if (_playerInputState.IsMovingDown) dir += Vector3.down;

            if (dir != Vector3.zero)
                _playerView.AddForce(dir * _settings.MoveSpeed);

            KeepPlayerOnScreen();
        }

        private void KeepPlayerOnScreen()
        {
            Vector3 pos = _playerView.Position;

            float left   = (_levelBoundary.Left   + _settings.BoundaryBuffer) - pos.x;
            float right  = pos.x - (_levelBoundary.Right  - _settings.BoundaryBuffer);
            float top    = pos.y - (_levelBoundary.Top    - _settings.BoundaryBuffer);
            float bottom = (_levelBoundary.Bottom + _settings.BoundaryBuffer) - pos.y;

            if (left > 0)
                _playerView.AddForce(Vector3.right * (_settings.BoundaryAdjustForce * left));

            if (right > 0)
                _playerView.AddForce(Vector3.left * (_settings.BoundaryAdjustForce * right));

            if (top > 0)
                _playerView.AddForce(Vector3.down * (_settings.BoundaryAdjustForce * top));

            if (bottom > 0)
                _playerView.AddForce(Vector3.up * (_settings.BoundaryAdjustForce * bottom));
        }
    }
}