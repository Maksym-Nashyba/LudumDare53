using System;
using Code.TrainInventory;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.Train
{
    public class Train : MonoBehaviour
    {
        [SerializeField] private GameObject _headVagonPrefab;
        [SerializeField] private GameObject _vagonPrefab;
        [SerializeField] private GameObject _bridgePrefab;
        protected DataBus DataBus;

        protected virtual void Awake()
        {
            DataBus = FindObjectOfType<DataBus>();
        }

        private void Start()
        {
            SpawnTrain();
        }

        private void SpawnTrain()
        {
            SpawnVagon(_headVagonPrefab, 0f);
            float offset = 11f;
            foreach (Inventory vagon in DataBus.Vagons)
            {
                SpawnBridge(offset);
                SpawnVagon(_vagonPrefab, offset);
                SpawnTrainObjects(offset, vagon);
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

        protected void SpawnTrainObjects(float offset, Inventory inventory)
        {
            Vector3 slotSize = new Vector3(8f / 9f, 0f, 4f / 5f);
            foreach (InventoryEntry entry in inventory.Entries)
            {
                GameObject instance = Instantiate(entry.Prefab);
                Vector3 position = new Vector3(-offset, 0f, 0f);
                position += new Vector3(-4f, 0f, 2f);
                position += new Vector3(slotSize.x * entry.TopLeftSlotPosition.x, 0f, 
                    slotSize.z *  -entry.TopLeftSlotPosition.y);
                Vector2Int size = new Vector2Int(Mathf.Clamp(entry.Size.x-1, 1, 9), 
                    Mathf.Clamp(entry.Size.y-1, 1, 9));
                position += new Vector3(slotSize.x * size.x, 0f, 
                    slotSize.z *  -size.y);
                instance.transform.position = position;
                entry.RealworldObject = instance;
            }
        }
    }
}
