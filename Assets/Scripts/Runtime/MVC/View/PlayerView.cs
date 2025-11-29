using UnityEngine;

namespace Runtime.MVC.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private MeshRenderer rend;

        public MeshRenderer Renderer => rend;

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

        public void AddForce(Vector3 force)
        {
            rb.AddForce(force);
        }
    }
}