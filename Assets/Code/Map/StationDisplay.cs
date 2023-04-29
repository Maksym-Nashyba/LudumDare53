using System;
using Code.Stations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Map
{
    public class StationDisplay : MonoBehaviour
    {
        public Station Station => _station;
        [SerializeField] private Station _station;
        [SerializeField] private TextMeshProUGUI _text;
        public Button Button => _button;
        [SerializeField] private Button _button;

        private void OnValidate()
        {
            if (_text != null && _station != null)
            {
                _text.text = _station.name;
                gameObject.name = _station.name;
            }
        }
    }
}