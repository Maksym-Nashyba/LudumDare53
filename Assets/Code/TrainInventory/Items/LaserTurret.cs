using System;
using System.Linq;
using UnityEngine;

namespace Code.TrainInventory.Items
{
    public class LaserTurret : Turret
    {
        [SerializeField] private int _maxHp;
        private int _hp;

        private void Awake()
        {
            _hp = _maxHp;
        }

        public override void DealDamage(int damage)
        {
            _hp = Mathf.Clamp(_hp - damage, 0, Int32.MaxValue);
            if (_hp < 1) Die();
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