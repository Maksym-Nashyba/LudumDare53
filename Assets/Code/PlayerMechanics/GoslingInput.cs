using UnityEngine;

namespace Code.PlayerMechanics
{
    public class GoslingInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _gosling;
        private Plane _xzPlane;

        private void Awake()
        {
            _xzPlane = new Plane(Vector3.up, Vector3.zero);
        }

        public bool TryGetMoveInput(out Vector2 input)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return input.magnitude > 0.1f;
        }
        
        public Vector3 GetMouseWorldPosition()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if(!_xzPlane.Raycast(ray, out float enterDistance)) return Vector3.zero;
            return ray.GetPoint(enterDistance);
        }

        public Vector3 GetLookDirection()
        {
            Vector3 direction = (GetMouseWorldPosition() - _gosling.position);
            direction.y = 0f;
            return direction;
        }
    }
}