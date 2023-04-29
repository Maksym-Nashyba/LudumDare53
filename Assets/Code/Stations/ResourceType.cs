using UnityEngine;

namespace Code.Stations
{
    [CreateAssetMenu(fileName = "RESOURCE", menuName = "SOs/Resource")]
    public class ResourceType : ScriptableObject
    {
        public string Name;
        public int StandartCost;
    }
}