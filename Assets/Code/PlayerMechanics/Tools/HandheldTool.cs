using UnityEngine;

namespace Code.PlayerMechanics.Tools
{
    public abstract class HandheldTool : MonoBehaviour
    {
        public ToolAnimationType AnimationType => _animationType;
        [SerializeField] private ToolAnimationType _animationType;

        public abstract void Use(Vector3 direction);

        protected bool HitsDamagable(Vector3 origin, Vector3 direction, float range, out IDamagable damagable, out Vector3 point)
        {
            damagable = null;
            point = Vector3.zero;
            Ray ray = new Ray(origin, direction);
            Debug.DrawRay(origin, direction, Color.magenta);
            if(!Physics.Raycast(ray, out RaycastHit hit, range, LayerMask.GetMask("DamageTarget")))return false;
            damagable = hit.collider.GetComponentInParent<IDamagable>();
            point = hit.point;
            return damagable != null;
        }
    }
}