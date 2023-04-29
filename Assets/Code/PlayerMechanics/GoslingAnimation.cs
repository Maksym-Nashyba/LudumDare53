using System;
using Code.PlayerMechanics.Tools;
using UnityEngine;

namespace Code.PlayerMechanics
{
    public class GoslingAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GoslingInventory _goslingInventory;
        [SerializeField] private GoslingMovement _goslingMovement;
        [SerializeField] private Transform _transform;
        
        private void Start()
        {
            _goslingInventory.ActiveToolChanged += OnToolChanged;
        }

        private void OnDestroy()
        {
            _goslingInventory.ActiveToolChanged -= OnToolChanged;
        }

        private void Update()
        {
            Vector3 lookDirection = _goslingMovement.GetLookDirection();
            float angle = Vector2.SignedAngle(Vector2.right, new Vector2(lookDirection.z, lookDirection.x));
            _transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _animator.SetFloat("Speed", _goslingMovement.GetSpeed());
        }

        private void OnToolChanged(HandheldTool nextTool)
        {
            if (nextTool.AnimationType == ToolAnimationType.Pistol)
            {
                _animator.SetLayerWeight(1, 1f);
                _animator.SetLayerWeight(1, 0f);
            }
            else
            {
                _animator.SetLayerWeight(1, 0f);
                _animator.SetLayerWeight(1, 1f);
            }
        }
    }
}
