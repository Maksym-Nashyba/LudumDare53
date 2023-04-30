using System.Collections;
using System.Linq;
using Code.Enemies;
using UnityEngine;

namespace Code.TrainInventory.Items
{
    public abstract class Turret : TrainObject
    {
        [SerializeField] private float _cdDuration;
        private bool _inCD;
        protected Enemy Enemy; 
        
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