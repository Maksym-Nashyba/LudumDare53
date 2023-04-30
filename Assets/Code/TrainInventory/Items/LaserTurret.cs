using System;
using System.Linq;
using UnityEngine;

namespace Code.TrainInventory.Items
{
    public class LaserTurret : Turret
    {
        [SerializeField] private int _maxHp;
        [SerializeField] private Transform _movingPart;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private int _damage;
        private EffectsManager _effectsManager;
        private Transform _transform;
        private int _hp;

        private void Awake()
        {
            _effectsManager = FindObjectOfType<EffectsManager>();
            _hp = _maxHp;
            _transform = transform;
        }

        protected override void Update()
        {
            base.Update();
            if(Enemy == null)return;
            Vector3 direction = Enemy.transform.position - _transform.position;
            float angle = Vector2.SignedAngle(Vector2.right, new Vector2(direction.z, direction.x)) + 180f;
            _movingPart.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        public override void DealDamage(int damage)
        {
            _hp = Mathf.Clamp(_hp - damage, 0, Int32.MaxValue);
            if (_hp < 1) Die();
        }

        protected override void Fire()
        {
            Enemy.DealDamage(_damage);
            _effectsManager.PlayShotEffect(_muzzle.position, Enemy.transform.position);
        }

        private void Die()
        {
            FindObjectOfType<DataBus>().Vagons.ForEach(inventory =>
            {
                if (inventory.Entries.Any(entry => entry.RealworldObject == gameObject))
                {
                    InventoryEntry entry = inventory.Entries.First(entry => entry.RealworldObject == gameObject);
                    inventory.RemoveEntry(entry);
                }
            });
            Destroy(gameObject);
        }
    }
}