﻿using System;
using System.Collections.Generic;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.TrainInventory
{
    public class Inventory : MonoBehaviour
    {
        public const int InventoryRows = 5;
        public const int InventoryColumns = 9;
        public event Action InventoryChanged;
        public event Action<Inventory> Changed;
        public bool[,] IsSlotsFill { get; private set; }

        public IReadOnlyCollection<InventoryEntry> Entries => _items.AsReadOnly();
        private List<InventoryEntry> _items;

        private void Awake()
        {
            IsSlotsFill = new bool[InventoryRows, InventoryColumns];
            _items = new List<InventoryEntry>();
        }

        public void AddEntry(InventoryEntry item, Vector2Int positionOnVagon) //TOP LEFT POSITION
        {
            item = Instantiate(item);
            if (!IsWithinInventoryBounds(positionOnVagon, item.Size)) throw new IndexOutOfRangeException();
            if (!AreSlotsAvailable(positionOnVagon, item.Size)) throw new InvalidOperationException();
            item.UpdatePosition(positionOnVagon);
            UpdateSlots(positionOnVagon, item.Size, true);
            _items.Add(item);
            InventoryChanged?.Invoke();
            Changed?.Invoke(this);
        }

        public InventoryEntry GetEntryInSlot(Vector2Int slot)
        {
            InventoryEntry itemInSlot = null;

            foreach (InventoryEntry item in _items)
            {
                for (int y = 0; y < item.Size.y; y++)
                {
                    for (int x = 0; x < item.Size.x; x++)
                    {
                        if (!(item.TopLeftSlotPosition.y + y == slot.y &&
                              item.TopLeftSlotPosition.x + x == slot.x)) continue;
                        itemInSlot = item;
                        break;
                    }
                }
            }

            return itemInSlot;
        }

        public void RemoveEntry(InventoryEntry item)
        {
            _items.Remove(item);
            UpdateSlots(item.TopLeftSlotPosition, item.Size, false);
            InventoryChanged?.Invoke();
            Changed?.Invoke(this);
        }

        private bool IsWithinInventoryBounds(Vector2Int topLeftItemSlot, Vector2Int itemSize)
        {
            bool isWithinBounds = true;
            for (int y = 0; y < itemSize.y; y++)
            {
                for (int x = 0; x < itemSize.x; x++)
                {
                    if (topLeftItemSlot.y + y < InventoryRows && topLeftItemSlot.x + x < InventoryColumns &&
                        topLeftItemSlot.y + y >= 0 && topLeftItemSlot.x + x >= 0) continue;
                    isWithinBounds = false;
                    break;
                }
            }

            return isWithinBounds;
        }

        private bool AreSlotsAvailable(Vector2Int topLeftItemSlot, Vector2Int itemSize)
        {
            bool canPlace = true;
            for (int y = 0; y < itemSize.y; y++)
            {
                for (int x = 0; x < itemSize.x; x++)
                {
                    if (!IsSlotsFill[topLeftItemSlot.y + y, topLeftItemSlot.x + x]) continue;
                    canPlace = false;
                    break;
                }
            }

            return canPlace;
        }

        private void UpdateSlots(Vector2Int topLeftItemSlot, Vector2Int itemSize, bool slotValue)
        {
            for (int y = 0; y < itemSize.y; y++)
            {
                for (int x = 0; x < itemSize.x; x++)
                {
                    IsSlotsFill[topLeftItemSlot.y + y, topLeftItemSlot.x + x] = slotValue;
                }
            }
        }
    }
}