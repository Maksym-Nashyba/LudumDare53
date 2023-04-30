using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.TrainInventory
{
    public class DragOperation
    {
        public InventoryEntry InventoryEntry { get; }
        public Vector2Int OriginalSlot { get; }
        private GameObject _draggedObject;

        public DragOperation(InventoryEntry inventoryEntry, GameObject draggedObject, Vector2Int originalSlot)
        {
            InventoryEntry = inventoryEntry;
            OriginalSlot = originalSlot;
            _draggedObject = draggedObject;
        }
    }
}