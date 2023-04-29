using System;
using System.Collections.Generic;
using Code.TrainInventory;
using UnityEngine;

namespace Code
{
    public class DataBus : MonoBehaviour
    {
        public List<Inventory> Vagons;
        public int Credits;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
