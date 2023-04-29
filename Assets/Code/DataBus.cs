using System;
using System.Collections.Generic;
using Code.TrainInventory;
using UnityEngine;

namespace Code
{
    public class DataBus : MonoBehaviour
    {
        public List<Inventory> Vagons;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
