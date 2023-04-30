using System;
using Code.PlayerMechanics;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.TrainInventory
{
    public class InventoryDragAndDrop : MonoBehaviour
    {
        private Inventory _inventory;
        [SerializeField] private InventoryDisplay _inventoryDisplay;
        [SerializeField] private GoslingInput _goslingInput;
        private DragOperation _dragOperation;
        private Vector2Int _selectedSlot;
        private Action _buyEntryAction;

        private void Awake()
        {
            _inventory = FindObjectOfType<DataBus>().Vagons[0];
        }

        private void Update()
        {
            Vector3 mouseWorldPosition = _goslingInput.GetMouseWorldPosition();
            _inventoryDisplay.UpdateInventorySlots();
            if (_dragOperation != null)
            {
                for (int y = 0; y < Inventory.InventoryRows; y++)
                {
                    for (int x = 0; x < Inventory.InventoryColumns; x++)
                    {
                        if(!_inventoryDisplay.InventorySlots[y, x].IsInSlot(mouseWorldPosition)) continue;
                        Vector2Int entrySlot = new Vector2Int(x, y);
                        _inventoryDisplay.UpdateInventorySlots(entrySlot, _dragOperation.InventoryEntry.Size);
                        break;
                    }
                }
            }
            if(Input.GetMouseButtonUp(0) && _dragOperation != null) EndDrag();
            if (!Input.GetMouseButtonDown(0)) return;
            if(_dragOperation != null) return;
            for (int y = 0; y < Inventory.InventoryRows; y++)
            {
                for (int x = 0; x < Inventory.InventoryColumns; x++)
                {
                    if(!_inventoryDisplay.InventorySlots[y, x].IsInSlot(mouseWorldPosition)) continue;
                    Vector2Int entrySlot = new Vector2Int(x, y);
                    InventoryEntry entry = _inventory.GetEntryInSlot(entrySlot);
                    StartDrag(entry, entrySlot);
                    return;
                }
            }
        }
        
        public void StartDrag(InventoryEntry entry, Action action)
        {
            if(_dragOperation != null) return;
            _dragOperation = new DragOperation(entry, entry.Prefab, new Vector2Int(0,0));
            _buyEntryAction = action;
        }

        private void StartDrag(InventoryEntry entry, Vector2Int entrySlot)
        {
            if(_dragOperation != null) return;
            _dragOperation = new DragOperation(entry, entry.Prefab, entrySlot);
            _inventory.RemoveEntry(entry);
        }

        private void EndDrag()
        {
            Vector2Int endDragSlot = new Vector2Int();
            Vector3 mouseWorldPosition = _goslingInput.GetMouseWorldPosition();
            for (int y = 0; y < Inventory.InventoryRows; y++)
            {
                for (int x = 0; x < Inventory.InventoryColumns; x++)
                {
                    if(!_inventoryDisplay.InventorySlots[y, x].IsInSlot(mouseWorldPosition)) continue;
                    endDragSlot = new Vector2Int(x, y);
                    break;
                }
            }

            _buyEntryAction?.Invoke();

            try
            {
                _inventory.AddEntry(_dragOperation.InventoryEntry, endDragSlot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _inventory.AddEntry(_dragOperation.InventoryEntry, _dragOperation.OriginalSlot);
            }
            _dragOperation = null;
        }
    }
}