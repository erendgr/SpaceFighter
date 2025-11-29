using Runtime.MVC.View;
using UnityEngine;
using Zenject;

namespace Runtime.MVC.Controller.Player
{
    public class PlayerRotateController : ITickable
    {
        readonly PlayerView _view;
        readonly Camera _mainCamera;

        public PlayerRotateController(PlayerView view, Camera mainCamera)
        {
            _view = view;
            _mainCamera = mainCamera;
        }
        
        public void Tick()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var mousePos = ray.origin;
            mousePos.z = 0;
            
            var targetDir = mousePos - _view.Position;
            targetDir.z = 0;
            targetDir.Normalize();
            
            _view.Rotation = Quaternion.LookRotation(targetDir) * Quaternion.AngleAxis(90, Vector3.up);
        }
    }
}