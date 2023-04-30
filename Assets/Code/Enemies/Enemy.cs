using System;
using Code.ActionPhase;
using Code.PlayerMechanics.Tools;
using UnityEngine;

namespace Code.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField] private Healthbar _healthbar;
        [SerializeField] private int _maxHp;
        private int _hp;

        protected virtual void Awake()
        {
            _hp = _maxHp;
        }

        public void DealDamage(int damage)
        {
            _hp = Mathf.Clamp(_hp - damage, 0, Int32.MaxValue);
            _healthbar.UpdateBar(Mathf.Clamp01(_hp/(float)_maxHp));
            if (_hp < 1) Die();
        }

        protected abstract void Die();
    }
}