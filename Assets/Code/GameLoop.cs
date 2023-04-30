using Code.Stations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class GameLoop : MonoBehaviour
    {
        private DataBus _dataBus;
        
        private void Awake()
        {
            _dataBus = FindObjectOfType<DataBus>();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SceneManager.LoadScene("Station");
        }

        public void StartTrackTowards(Station station)
        {
            _dataBus.CurrentStation = station;
            SceneManager.LoadScene("Action");
        }
    }
}