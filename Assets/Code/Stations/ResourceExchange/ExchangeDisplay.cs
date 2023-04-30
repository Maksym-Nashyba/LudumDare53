using System.Collections.Generic;
using Code.TrainInventory.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Stations.ResourceExchange
{
    public class ExchangeDisplay : MonoBehaviour
    {
        [SerializeField] private Exchange _exchange;
        [SerializeField] private GameObject _productButtonPrefab;

        [SerializeField] private Button _resourcesButton;
        [SerializeField] private Button _turretsButton;
        [SerializeField] private Button _upgradesButton;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        private List<ExchangeProductButton> _buttons;

        private void Awake()
        {
            _buttons = new List<ExchangeProductButton>();
            _resourcesButton.onClick.AddListener(() =>
            {
                UpdateResourcesTab(_exchange.CheapResources, _exchange.ExpensiveResources);
            });
            _turretsButton.onClick.AddListener(() =>
            {
                UpdateTurretsTab(_exchange.AvailableTurrets);
            });
            _upgradesButton.onClick.AddListener(UpdateUpgradesTab);
            _inventoryButton.onClick.AddListener(() =>
            {
                UpdateInventoryTab(_exchange.GetInventoryResources());
            });
        }

        private void OnDestroy()
        {
            RemoveButtonsListeners();
            _resourcesButton.onClick.RemoveAllListeners();
            _turretsButton.onClick.RemoveAllListeners();
            _upgradesButton.onClick.RemoveAllListeners();
            _inventoryButton.onClick.RemoveAllListeners();
        }

        private void UpdateResourcesTab(InventoryEntry[] cheapResources, InventoryEntry[] expensiveResources)
        {
            RemoveButtonsListeners();
            foreach (InventoryEntry resource in cheapResources)
            {
                    ExchangeProductButton button = 
                        Instantiate(_productButtonPrefab, _verticalLayoutGroup.transform).GetComponent<ExchangeProductButton>();
                    button.ProductName.text = resource.Name;
                    button.Price.text = (resource.StandartCost * 0.6f).ToString();
                    button.ButtonText.text = "Buy";
                    button.Button.onClick.AddListener(() =>
                    {
                        _exchange.BuyEntry(resource);
                    });
                    _buttons.Add(button);
            }
            foreach (InventoryEntry resource in expensiveResources)
            {
                ExchangeProductButton button = 
                    Instantiate(_productButtonPrefab, _verticalLayoutGroup.transform).GetComponent<ExchangeProductButton>();
                button.ProductName.text = resource.Name;
                button.Price.text = (resource.StandartCost * 1.4f).ToString();
                button.ButtonText.text = "Buy";
                button.Button.onClick.AddListener(() =>
                {
                    _exchange.BuyEntry(resource);
                });
                _buttons.Add(button);
            }
        }
        
        private void UpdateTurretsTab(InventoryEntry[] turrets)
        {
            RemoveButtonsListeners();
            foreach (InventoryEntry entry in turrets)
            {
                ExchangeProductButton button = 
                    Instantiate(_productButtonPrefab, _verticalLayoutGroup.transform).GetComponent<ExchangeProductButton>();
                button.ProductName.text = entry.Name;
                button.Price.text = entry.StandartCost.ToString();
                button.ButtonText.text = "Buy";
                button.Button.onClick.AddListener(() =>
                {
                    _exchange.BuyEntry(entry);
                });
                _buttons.Add(button);
            }
        }

        private void UpdateUpgradesTab()
        {
            
        }
        
        private void UpdateInventoryTab(InventoryEntry[] entries)
        {
            RemoveButtonsListeners();
            foreach (InventoryEntry entry in entries)
            {
                ExchangeProductButton button = 
                    Instantiate(_productButtonPrefab, _verticalLayoutGroup.transform).GetComponent<ExchangeProductButton>();
                button.ProductName.text = entry.Name;
                button.Price.text = (entry.StandartCost * _exchange.GetPriceMultiplier(entry)).ToString();
                button.ButtonText.text = "Sell";
                button.Button.onClick.AddListener(() =>
                {
                    _exchange.SellEntry(entry);
                    Destroy(button.gameObject);
                }); 
                _buttons.Add(button);
            }
        }

        private void RemoveButtonsListeners()
        {
            if(_buttons == null) return;
            foreach (ExchangeProductButton button in _buttons)
            {
                button.Button.onClick.RemoveAllListeners();
            }
            _buttons.Clear();
        }
    }
}