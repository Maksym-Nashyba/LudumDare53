using System;
using System.Linq;
using UnityEngine;

namespace Code.TrainInventory.Items
{
    public class LaserTurret : Turret
    {
        [SerializeField] private Transform _movingPart;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private int _damage;
        private EffectsManager _effectsManager;
        private Transform _transform;

        protected override void Awake()
        {
            base.Awake();
            _effectsManager = FindObjectOfType<EffectsManager>();
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

        protected override void Fire()
        {
            Enemy.DealDamage(_damage);
            _effectsManager.PlayLargeShotEffect(_muzzle.position, Enemy.transform.position);
        }

        protected override void Die()
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