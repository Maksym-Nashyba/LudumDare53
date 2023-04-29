using System.Collections;
using UnityEngine;

namespace Code.PlayerMechanics.Tools
{
    public class Pistol : HandheldTool
    {
        [SerializeField] private EffectsManager _effects;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _cooldownDuration;
        [SerializeField] private int _damage;
        private bool _inCooldown;

        public override void Use(Vector3 direction)
        {
            if(_inCooldown)return;
            Shoot(direction);
            StartCoroutine(PlayCooldown());
        }

        private void Shoot(Vector3 direction)
        {
            Vector3 leveledMuzzlePosition = _muzzle.position;
            leveledMuzzlePosition.y = Constants.PlayFieldHeight;
            if (HitsDamagable(leveledMuzzlePosition, direction, out IDamagable damagable, out Vector3 point))
            {
                damagable.Damage(_damage);
                _effects.PlayShotEffect(_muzzle.position, point);
            }
            else
            {
                _effects.PlayShotEffectNoTarget(_muzzle.position, direction);
            }
        }

        private IEnumerator PlayCooldown()
        {
            _inCooldown = true;
            float passed = 0f; 
            while (passed < _cooldownDuration)
            {
                yield return null;
                passed += Time.deltaTime;
            }

            _inCooldown = false;
        }
    }
}