using System.Collections;
using System.Linq;
using Code.Train;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.Enemies
{
    public class MeleeEnemy : Enemy
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private int _damage;
        [SerializeField] private float _cdDurationSeconds;
        [SerializeField] private float _speed;
        private bool _inCD = false;
        private ActionTrain _train;
        private TrainObject _target;

        protected override void Awake()
        {
            base.Awake();
            _train = FindObjectOfType<ActionTrain>();
        }

        private void Start()
        {
            _target = FindTarget();
        }

        private void Update()
        {
            if (_target == null)
            {
                _target = FindTarget();
                return;
            }

            if (GetDistanceToCurrentTarget() < 1.5f)
            {
                if (!_inCD)Attack(_target);
            }
            else
            {
                MoveTowardsTarget();
            }
        }

        private void Attack(TrainObject target)
        {
            target.DealDamage(_damage);
            StartCoroutine(PlayCD());
        }

        private void MoveTowardsTarget()
        {
            Vector3 direction = _target.transform.position - _rigidbody.position;
            _rigidbody.MovePosition(_rigidbody.position + direction.normalized * (Time.deltaTime * _speed));
        }
        
        private float GetDistanceToCurrentTarget()
        {
            return (_target.transform.position - _rigidbody.position).magnitude;
        }

        private IEnumerator PlayCD()
        {
            float passed = 0f;
            _inCD = true;
            while (passed < _cdDurationSeconds)
            {
                yield return null;
                passed += Time.deltaTime;
            }

            _inCD = false;
        }
        
        private TrainObject FindTarget()
        {
            if (_train.Cargoes.Count != 0)
            {
                Cargo closestCargo = _train.Cargoes
                    .OrderBy(cargo => (cargo.transform.position - _rigidbody.position).magnitude).First();
                return closestCargo;
            }
            
            if (_train.Turrets.Count == 0) return null;
            Turret closestTurret = _train.Turrets
                .OrderBy(turret => (turret.transform.position - _rigidbody.position).magnitude).First();
            return closestTurret;
        }

        protected override void Die()
        {
            Destroy(gameObject);
        }
    }
}