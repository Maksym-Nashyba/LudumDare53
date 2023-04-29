using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.TrainInventory
{
    public class Inventory : MonoBehaviour
    {
        private const int InventoryHorizontalSize = 8;
        private const int InventoryVerticalSize = 4;
        
        private bool[,] _isSlotsFill;
        private List<InventoryItem> _items;

        private void Awake()
        {
            _isSlotsFill = new bool[InventoryHorizontalSize, InventoryVerticalSize];
        }

        public void AddItem(InventoryItem item, Vector2Int topLeftItemSlot)
        {
            if (!CheckSlots(topLeftItemSlot, item.Size)) throw new InvalidOperationException();
            item.UpdatePosition(topLeftItemSlot);
            UpdateSlots(topLeftItemSlot, item.Size, true);
            _items.Add(item);
        }

        public void RemoveItem(InventoryItem item)
        {
            _items.Remove(item);
            UpdateSlots(item.TopLeftSlotPosition, item.Size, false);
            Destroy(item);
        }

        private bool CheckSlots(Vector2Int topLeftItemSlot, Vector2Int itemSize)
        {
            bool canPlace = true;
            for (int x = 0; x < itemSize.x; x++)
            {
                for (int y = 0; y < itemSize.y; y++)
                {
                    if (!_isSlotsFill[topLeftItemSlot.x + x, topLeftItemSlot.y + y]) continue;
                    canPlace = false;
                    break;
                }
            }

            return canPlace;
        }

        private void UpdateSlots(Vector2Int topLeftItemSlot, Vector2Int itemSize, bool slotValue)
        {
            for (int x = 0; x < itemSize.x; x++)
            {
                for (int y = 0; y < itemSize.y; y++)
                {
                    _isSlotsFill[topLeftItemSlot.x + x, topLeftItemSlot.y + y] = slotValue;
                }
            }
        }
    }
}