using UnityEngine;

namespace Code.TrainInventory.Items
{
    [CreateAssetMenu(fileName = "INV_ENTRY", menuName = "SOs/InventoryEntry")]
    public class InventoryEntry : ScriptableObject
    {
        public Vector2Int Size => _size;
        public GameObject Prefab => _prefab;
        public Vector2Int TopLeftSlotPosition { get; private set; }
        public GameObject RealworldObject;
        [SerializeField] private Vector2Int _size;
        [SerializeField] private GameObject _prefab;

        public void UpdatePosition(Vector2Int topLeftSlotPosition)
        {
            TopLeftSlotPosition = topLeftSlotPosition;
        }
    }
}