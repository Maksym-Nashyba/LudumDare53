using Code.TrainInventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Code.TrainInventory
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private GameLoop _inventorySlotPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        private InventorySlot[,] _inventorySlots;

        private void Awake()
        {
            _inventory.InventoryChanged += UpdateInventorySlots;
            GenerateInventorySlots();
        }

        private void OnDestroy()
        {
            _inventory.InventoryChanged -= UpdateInventorySlots;
        }

        public void AddItem(InventoryEntry item, Vector2Int topLeftItemSlot)
        {
            _inventory.AddItem(item, topLeftItemSlot);
            UpdateInventorySlots();
        }

        public void RemoveItem(InventoryEntry item)
        {
            _inventory.RemoveItem(item);
            UpdateInventorySlots();
        }

        private void GenerateInventorySlots()
        {
            _inventorySlots = new InventorySlot[Inventory.InventoryRows, Inventory.InventoryColumns];
            for (int y = 0; y < Inventory.InventoryRows; y++)
            {
                for (int x = 0; x < Inventory.InventoryColumns; x++)
                {
                    _inventorySlots[y, x] = GenerateInventorySlot();
                }
            }
        }

        private InventorySlot GenerateInventorySlot()
        {
            return Instantiate(_inventorySlotPrefab, _gridLayoutGroup.transform).GetComponent<InventorySlot>();
        }

        private void UpdateInventorySlots()
        {
            for (int y = 0; y < Inventory.InventoryRows; y++)
            {
                for (int x = 0; x < Inventory.InventoryColumns; x++)
                {
                    if (_inventory.IsSlotsFill[y,x]) _inventorySlots[y,x].FillSlot();
                    else _inventorySlots[y,x].ClearSlot();
                }
            }
        }
    }
}