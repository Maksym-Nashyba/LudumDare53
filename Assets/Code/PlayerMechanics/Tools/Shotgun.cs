using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.PlayerMechanics.Tools
{
    public class Shotgun : HandheldTool
    {
        [SerializeField] private EffectsManager _effects;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _cooldownDuration;
        [SerializeField] private int _damage;
        private bool _inCooldown;
        
        public override void Use(Vector3 direction)
        {
            if(_inCooldown)return;
            Shoot(Quaternion.Euler(0f, -15f, 0f) * direction);
            Shoot(Quaternion.Euler(0f, -7.5f, 0f) * direction);
            Shoot(direction);
            Shoot(Quaternion.Euler(0f, 7.5f, 0f) * direction);
            Shoot(Quaternion.Euler(0f, 15f, 0f) * direction);
            StartCoroutine(PlayCooldown());
        }
        
        private void Shoot(Vector3 direction)
        {
            direction.y = 0f;
            direction.Normalize();
            Vector3 leveledMuzzlePosition = _muzzle.position;
            leveledMuzzlePosition.y = Constants.PlayFieldHeight;
            if (HitsDamagable(leveledMuzzlePosition, direction, out IDamagable damagable, out Vector3 point))
            {
                damagable.DealDamage(_damage);
                _effects.PlayShotEffect(_muzzle.position, point);
            }
            else
            {
                _effects.PlayShotEffect(_muzzle.position, _muzzle.position + (direction * 5f));
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