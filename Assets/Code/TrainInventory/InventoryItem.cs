using UnityEngine;

namespace Code.TrainInventory
{
    public abstract class InventoryItem : MonoBehaviour
    {
        public Vector2Int Size => _size;
        public Vector2Int TopLeftSlotPosition { get; private set; }
        [SerializeField] private Vector2Int _size;

        public void UpdatePosition(Vector2Int topLeftSlotPosition)
        {
            TopLeftSlotPosition = topLeftSlotPosition;
        }
    }
}