using System;
using System.Collections.Generic;
using Code.TrainInventory.Items;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI _balanceAmount;
        private List<ExchangeProductButton> _buttons;

        private void Awake()
        {
            _exchange.BalanceChanged += OnBalanceChanged;
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

        private void Start()
        {
            UpdateResourcesTab(_exchange.CheapResources, _exchange.ExpensiveResources);
        }

        private void OnDestroy()
        {
            _exchange.BalanceChanged -= OnBalanceChanged;
            ClearButtons();
            _resourcesButton.onClick.RemoveAllListeners();
            _turretsButton.onClick.RemoveAllListeners();
            _upgradesButton.onClick.RemoveAllListeners();
            _inventoryButton.onClick.RemoveAllListeners();
        }

        private void OnBalanceChanged(int balance)
        {
            _balanceAmount.text = balance.ToString();
        }

        private void UpdateResourcesTab(InventoryEntry[] cheapResources, InventoryEntry[] expensiveResources)
        {
            ClearButtons();
            foreach (InventoryEntry entry in cheapResources)
            {
                SpawnProductButton(entry.Name, entry.StandartCost * 0.6f, "Buy", button => _exchange.BuyEntry(entry));
            }
            foreach (InventoryEntry entry in expensiveResources)
            {
                SpawnProductButton(entry.Name, entry.StandartCost * 1.4f, "Buy", button => _exchange.BuyEntry(entry));
            }
        }
        
        private void UpdateTurretsTab(InventoryEntry[] entries)
        {
            ClearButtons();
            foreach (InventoryEntry entry in entries)
            {
                SpawnProductButton(entry.Name, entry.StandartCost, "Buy", button => _exchange.BuyEntry(entry));
            }
        }

        private void UpdateUpgradesTab()
        {
            ClearButtons();
            foreach ((string name, int price) upgrade in _exchange.GetAvailableUpgrades())
            {
                SpawnProductButton(upgrade.name, upgrade.price, "Buy", button =>
                {
                    if(!_exchange.BuyUpgrade(upgrade.name)) return;
                    button.Button.onClick.RemoveAllListeners();
                    _buttons.Remove(button);
                    Destroy(button.gameObject);
                });
            }
        }
        
        private void UpdateInventoryTab(InventoryEntry[] entries)
        {
            ClearButtons();
            foreach (InventoryEntry entry in entries)
            {
                SpawnProductButton(entry.Name, entry.StandartCost * _exchange.GetPriceMultiplier(entry),
                    "Sell", button =>
                    {
                        _exchange.SellEntry(entry);
                        button.Button.onClick.RemoveAllListeners();
                        _buttons.Remove(button);
                        Destroy(button.gameObject);
                    });
            }
        }

        private void ClearButtons()
        {
            if(_buttons == null || _buttons.Count == 0) return;
            foreach (ExchangeProductButton button in _buttons)
            {
                button.Button.onClick.RemoveAllListeners();
                Destroy(button.gameObject);
            }
            _buttons.Clear();
        }

        private void SpawnProductButton(string productName, float price, string buttonText, Action<ExchangeProductButton> onClickAction)
        {
            ExchangeProductButton button = 
                Instantiate(_productButtonPrefab, _verticalLayoutGroup.transform).GetComponent<ExchangeProductButton>();
            button.ProductName.text = productName;
            button.Price.text = price.ToString();
            button.ButtonText.text = buttonText;
            button.Button.onClick.AddListener(() =>
            {
                onClickAction?.Invoke(button);
            });
            _buttons.Add(button);
        }
    }
}