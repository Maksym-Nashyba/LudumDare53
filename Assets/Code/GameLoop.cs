using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code
{
    public class GameLoop : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SceneManager.LoadScene("Station");
        }
    }
}
