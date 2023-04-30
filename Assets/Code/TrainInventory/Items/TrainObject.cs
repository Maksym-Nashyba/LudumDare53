using System;
using Code.PlayerMechanics.Tools;
using Code.Train;
using UnityEngine;

namespace Code.TrainInventory.Items
{
    public abstract class TrainObject : MonoBehaviour, IDamagable
    {
        public abstract void DealDamage(int damage);

        private void OnDestroy()
        {
            FindObjectOfType<ActionTrain>()?.Remove(this);
        }
    }
}