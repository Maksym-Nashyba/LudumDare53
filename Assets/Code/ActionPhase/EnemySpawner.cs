using System.Collections;
using Code.Train;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.ActionPhase
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _enemyPrefabs;
        private ActionTrain _train;

        private void Awake()
        {
            _train = FindObjectOfType<ActionTrain>();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemy(1f));
        }

        private IEnumerator SpawnEnemy(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (gameObject == null || _train.Turrets.Count == 0) yield break;

            Vector3 center = _train.Turrets[Random.Range(0, _train.Turrets.Count)].transform.position;
            float offset = Random.Range(0f,2f) >1f ? 10f : -10f;
            center += new Vector3(0f, 0f, offset);
            GameObject istance = Instantiate(_enemyPrefabs[0], center, Quaternion.identity);
            StartCoroutine(SpawnEnemy(Random.Range(3f, 7f)));
        } 
    }
}