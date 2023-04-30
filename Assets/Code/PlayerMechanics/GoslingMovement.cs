using UnityEngine;

namespace Code.PlayerMechanics
{
    [RequireComponent(typeof(Rigidbody))]
    public class GoslingMovement : MonoBehaviour
    {
        [SerializeField] private float _speedUnitsPerSecond;
        [SerializeField] private GoslingInput _input;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(!_input.TryGetMoveInput(out Vector2 input))return;

            Vector3 direction = new Vector3(input.x, 0f, input.y);
            Vector3 force = direction.normalized * (_speedUnitsPerSecond * Time.deltaTime);
            _rigidbody.MovePosition(_rigidbody.position + force);
        }
        
        public float GetSpeed()
        {
            _input.TryGetMoveInput(out Vector2 input);
            return input.magnitude;
        }

        public Vector3 GetLookDirection()
        {
            return _input.GetMouseWorldPosition() - _rigidbody.position;
        }
    }
}
