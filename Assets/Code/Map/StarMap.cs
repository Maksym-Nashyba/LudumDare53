using System.Linq;
using Code.Stations;
using UnityEngine;

namespace Code.Map
{
    public class StarMap : MonoBehaviour
    {
        [SerializeField] private StationDisplay[] _displays;
        private GameLoop _gameLoop;

        private void Awake()
        {
            _gameLoop = FindObjectOfType<GameLoop>();
        }

        private void OnValidate()
        {
            _displays = GetComponentsInChildren<StationDisplay>();
        }

        private void Start()
        {
            foreach (StationDisplay display in _displays)
            {
                display.Button.onClick.AddListener(() => OnStationClicked(display.Station));
                display.Button.interactable = false;
            }

            foreach (Station station in FindObjectOfType<DataBus>().CurrentStation.Links)
            {
                _displays.First(display => display.Station == station).Button.interactable = true;
            }
        }

        private void OnDestroy()
        {
            foreach (StationDisplay display in _displays)
            {
                display.Button.onClick.RemoveAllListeners();
            }
        }

        private void OnStationClicked(Station station)
        {
            _gameLoop.StartTrackTowards(station);
        }
    }
}
