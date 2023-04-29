using System;
using System.Collections.Generic;
using System.Linq;
using Code.Stations;
using Code.TrainInventory;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code
{
    public class DataBus : MonoBehaviour
    {
        public List<Inventory> Vagons;
        public int Credits;
        public Station CurrentStation;

        [SerializeField] private InventoryEntry _turret;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            Vagons.First().AddItem(_turret, new Vector2Int(1,1));
            
            Vagons.First().AddItem(_turret, new Vector2Int(5,1));
            
            Vagons.First().AddItem(_turret, new Vector2Int(1,4));
        }
    }
}