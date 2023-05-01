using System;
using System.Collections;
using System.Linq;
using Code.ActionPhase;
using Code.Enemies;
using UnityEngine;

namespace Code.TrainInventory.Items
{
    public abstract class Turret : TrainObject
    {
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private float _cdDuration;
        [SerializeField] private int _maxHp;
        private bool _inCD;
        private int _hp;
        protected Enemy Enemy;

        protected virtual void Awake()
        {
            _hp = _maxHp;
        }

        protected abstract void Fire();

        protected virtual void Update()
        {
            if (Enemy == null)
            {
                Enemy = FindEnemy();
                return;
            }

            if (_inCD) return;
            Fire();
            StartCoroutine(PlayCD(_cdDuration));
        }

        public override void DealDamage(int damage)
        {
            _hp = Mathf.Clamp(_hp - damage, 0, Int32.MaxValue);
            _healthbar.UpdateBar(Mathf.Clamp01(_hp/(float)_maxHp));
            if (_hp < 1) Die();
        }

        protected abstract void Die();
        
        private IEnumerator PlayCD(float duration)
        {
            float passed = 0f;
            _inCD = true;
            while (passed < duration)
            {
                yield return null;
                passed += Time.deltaTime;
            }
            _inCD = false;
        }
        
        private Enemy FindEnemy()
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .Where(enemy => (enemy.transform.position - transform.position).magnitude < 35f).ToArray();
            if (enemies.Length == 0) return null;
            return enemies.OrderBy(enemy => (enemy.transform.position - transform.position).magnitude).First();
        }
    }
}