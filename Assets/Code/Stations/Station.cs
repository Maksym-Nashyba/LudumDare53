using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.Stations
{
    [CreateAssetMenu(fileName = "STATION", menuName = "SOs/Station")]
    public class Station : ScriptableObject
    {
        public Station[] Links;
        public InventoryEntry[] CheapResources;
        public InventoryEntry[] ExpensiveResources;
    }
}