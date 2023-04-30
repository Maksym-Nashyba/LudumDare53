using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class ActionLoop : MonoBehaviour
    {
        [SerializeField] private float _duration;
        private float _passed;
        private DataBus _dataBus;

        private void Awake()
        {
            _dataBus = FindObjectOfType<DataBus>();
        }

        private void Update()
        {
            _passed += Time.deltaTime;
            if (_passed >= _duration)
            {
                ReachStation();
                return;
            }
        }

        private void ReachStation()
        {
            SceneManager.LoadScene("Station");
        }
    }
}