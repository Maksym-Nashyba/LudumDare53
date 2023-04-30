using System.Collections.Generic;
using System.Linq;
using Code.Stations;
using Code.TrainInventory;
using Code.TrainInventory.Items;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class DataBus : MonoBehaviour
    {
        public List<Inventory> Vagons;
        public int Credits;
        public Station CurrentStation;
        public bool Shotgun;
        public bool Sniper;
        public bool Welder;

        [SerializeField] private InventoryEntry _turret;

        public void AddVagon()
        {
            Vagons.Add(gameObject.AddComponent<Inventory>());
            SceneManager.LoadScene("Station");
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            Vagons.First().AddEntry(_turret, new Vector2Int(1,1));
            
            Vagons.First().AddEntry(_turret, new Vector2Int(5,2));
        }
    }
}