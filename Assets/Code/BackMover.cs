using System;
using UnityEngine;

namespace Code
{
    public class BackMover : MonoBehaviour
    {
        private void Update()
        {
            transform.position += new Vector3(-Time.deltaTime/3f, 0f, 0f);
        }
    }
}