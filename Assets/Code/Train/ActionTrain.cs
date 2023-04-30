using System.Collections.Generic;
using System.Linq;
using Code.TrainInventory.Items;
using UnityEngine;

namespace Code.Train
{
    public class ActionTrain : Train
    {
        private List<TrainObject> _trainTargets;
        public List<Turret> Turrets;
        public List<Cargo> Cargoes;
        
        protected override void Start()
        {
            base.Start();
            _trainTargets = FindObjectsByType<TrainObject>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            Turrets = _trainTargets.OfType<Turret>().ToList();
            Cargoes = _trainTargets.OfType<Cargo>().ToList();
        }
        
        public void Remove(TrainObject trainObject)
        {
            _trainTargets.Remove(trainObject);
            if (trainObject is Turret turret) Turrets.Remove(turret);
            else Cargoes.Remove((Cargo)trainObject);
        }
    }
}