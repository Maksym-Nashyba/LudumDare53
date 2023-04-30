using System.Collections;
using System.Linq;
using Code.TrainInventory;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.Stations.ResourceExchange
{
    public class Exchange : MonoBehaviour
    {
        public InventoryEntry[] CheapResources => _dataBus.CurrentStation.CheapResources;
        public InventoryEntry[] ExpensiveResources => _dataBus.CurrentStation.ExpensiveResources;
        public InventoryEntry[] AvailableTurrets => _availableTurrets;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private InventoryEntry[] _availableTurrets;
        [SerializeField] private InventoryDragAndDrop _dragAndDrop;
        private DataBus _dataBus;

        private void Awake()
        {
            _dataBus = FindObjectOfType<DataBus>();
        }

        public void BuyEntry(InventoryEntry entry)
        {
            int entryPrice = (int) (entry.StandartCost * GetPriceMultiplier(entry));
            if (entryPrice > _dataBus.Credits) return;
            StartCoroutine(StartDrag(entry, entryPrice));
        }

        private IEnumerator StartDrag(InventoryEntry entry, int entryPrice)
        {
            yield return new WaitForSecondsRealtime(0.15f);
            _dragAndDrop.StartDrag(entry, () =>
            {
                _dataBus.Credits -= entryPrice;
            });
        }
        
        public void SellEntry(InventoryEntry entry)
        {
            _inventory.RemoveEntry(entry);
            _dataBus.Credits += (int)(entry.StandartCost * GetPriceMultiplier(entry));
        }

        public float GetPriceMultiplier(InventoryEntry entry)
        {
            float priceMultiplier = 1f;
            if (CheapResources.Any(resource => resource.name == entry.Name)) priceMultiplier = 0.6f;
            else if (ExpensiveResources.Any(resource => resource.name == entry.Name)) priceMultiplier = 1.4f;
            _dataBus.Credits += (int)(entry.StandartCost * priceMultiplier);
            return priceMultiplier;
        }

        public InventoryEntry[] GetInventoryResources()
        {
            return _inventory.Entries.ToArray();
        }
    }
}