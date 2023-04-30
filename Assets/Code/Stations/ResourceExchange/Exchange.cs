using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.TrainInventory;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.Stations.ResourceExchange
{
    public class Exchange : MonoBehaviour
    {
        public event Action<int> BalanceChanged;
        public InventoryEntry[] CheapResources => _dataBus.CurrentStation.CheapResources;
        public InventoryEntry[] ExpensiveResources => _dataBus.CurrentStation.ExpensiveResources;
        public InventoryEntry[] AvailableTurrets => _availableTurrets;
        [SerializeField] private InventoryEntry[] _availableTurrets;
        [SerializeField] private InventoryDragAndDrop _dragAndDrop;
        [SerializeField] private StationCamera _stationCamera;
        private Inventory _inventory;
        private DataBus _dataBus;

        private void Awake()
        {
            _dataBus = FindObjectOfType<DataBus>();
            _stationCamera.VagonChanged += OnVagonChanged;
            BalanceChanged?.Invoke(_dataBus.Credits);
        }

        private void OnDestroy()
        {
            _stationCamera.VagonChanged -= OnVagonChanged;
        }

        public bool BuyUpgrade(string upgradeName)
        {
            switch (upgradeName)
            {
                case "Shotgun":
                    if (_dataBus.Credits < 400) return false;
                    _dataBus.Credits -= 400;
                    _dataBus.Shotgun = true;
                    break;
                case "Sniper":
                    if (_dataBus.Credits < 550) return false;
                    _dataBus.Credits -= 550;
                    _dataBus.Sniper = true;
                    break;
                case "Welder":
                    if (_dataBus.Credits < 300) return false;
                    _dataBus.Credits -= 300;
                    _dataBus.Welder = true;
                    break;
                case "Vagon":
                    if (_dataBus.Credits < 500) return false;
                    _dataBus.Credits -= 500;
                    _dataBus.AddVagon();
                    break;
            }
            BalanceChanged?.Invoke(_dataBus.Credits);
            return true;
        }

        public (string, int)[] GetAvailableUpgrades()
        {
            List<(string, int)> upgrades = new List<(string, int)>();
            if(!_dataBus.Shotgun) upgrades.Add(("Shotgun", 400));
            if(!_dataBus.Sniper) upgrades.Add(("Sniper", 550));
            if(!_dataBus.Welder) upgrades.Add(("Welder", 300));
            upgrades.Add(("Vagon", 500));
            return upgrades.ToArray();
        }

        public bool BuyEntry(InventoryEntry entry)
        {
            int entryPrice = (int) (entry.StandartCost * GetPriceMultiplier(entry));
            if (entryPrice > _dataBus.Credits) return false;
            StartCoroutine(StartDrag(entry, entryPrice));
            return true;
        }

        public void SellEntry(InventoryEntry entry)
        {
            _inventory.RemoveEntry(entry);
            _dataBus.Credits += (int)(entry.StandartCost * GetPriceMultiplier(entry));
            BalanceChanged?.Invoke(_dataBus.Credits);
        }

        public float GetPriceMultiplier(InventoryEntry entry)
        {
            float priceMultiplier = 1f;
            if (CheapResources.Any(resource => resource.name == entry.Name)) priceMultiplier = 0.6f;
            else if (ExpensiveResources.Any(resource => resource.name == entry.Name)) priceMultiplier = 1.4f;
            return priceMultiplier;
        }

        public InventoryEntry[] GetInventoryResources()
        {
            return _inventory.Entries.ToArray();
        }
        
        private void OnVagonChanged(int currentVagonId)
        {
            _inventory = _dataBus.Vagons[currentVagonId];
        }

        private IEnumerator StartDrag(InventoryEntry entry, int entryPrice)
        {
            yield return new WaitForSecondsRealtime(0.15f);
            _dragAndDrop.StartDrag(entry, () =>
            {
                _dataBus.Credits -= entryPrice;
                BalanceChanged?.Invoke(_dataBus.Credits);
            });
        }
    }
}