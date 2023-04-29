using Code.TrainInventory;
using UnityEngine;

namespace Code.Train
{
    public class Train : MonoBehaviour
    {
        [SerializeField] private GameObject _headVagonPrefab;
        [SerializeField] private GameObject _vagonPrefab;
        [SerializeField] private GameObject _bridgePrefab;
        private DataBus _dataBus;

        private void Awake()
        {
            _dataBus = FindObjectOfType<DataBus>();
        }

        private void Start()
        {
            SpawnTrain();
        }

        private void SpawnTrain()
        {
            SpawnVagon(_headVagonPrefab, 0f);
            float offset = 11f;
            foreach (Inventory vagon in _dataBus.Vagons)
            {
                SpawnBridge(offset);
                SpawnVagon(_vagonPrefab, offset);
                offset += 11f;
            }
        }

        private void SpawnVagon(GameObject prefab, float offset)
        {
            Instantiate(prefab, new Vector3(-offset, 0f, 0f), Quaternion.identity);
        }

        private void SpawnBridge(float offset)
        {
            Instantiate(_bridgePrefab, new Vector3(-offset + 5.5f, 0.0625f, 0f), Quaternion.identity);
        }
    }
}
