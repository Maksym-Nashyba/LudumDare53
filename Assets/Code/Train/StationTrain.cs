using System;
using System.Security.Cryptography;
using Code.TrainInventory;
using Code.TrainInventory.Items;

namespace Code.Train
{
    public class StationTrain : Train
    {
        protected override void Awake()
        {
            base.Awake();
            foreach (Inventory inventory in DataBus.Vagons)
            {
                inventory.Changed += OnInventoryChanged;
            }
        }

        private void OnDestroy()
        {
            foreach (Inventory inventory in DataBus.Vagons)
            {
                inventory.Changed -= OnInventoryChanged;
            }
        }
        
        private void OnInventoryChanged(Inventory vagon)
        {
            foreach (InventoryEntry entry in vagon.Entries)
            {
                Destroy(entry.RealworldObject);
            }

            int vagonIndex = DataBus.Vagons.IndexOf(vagon);
            SpawnTrainObjects(11 * (vagonIndex + 1), vagon);
        }
    }
}