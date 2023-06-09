using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Code.Stations
{
    public class StationCamera : MonoBehaviour
    {
        public event Action<int> VagonChanged;
        [SerializeField] private CinemachineVirtualCamera _firstCamera;
        private List<CinemachineVirtualCamera> _cameras;
        private DataBus _dataBus;

        private int _currentCamIndex;
        
        private void Awake()
        {
            _dataBus = FindObjectOfType<DataBus>();
            _cameras = new List<CinemachineVirtualCamera>();
            _cameras.Add(_firstCamera);
            float offset = -11f;
            for (int i = 0; i < _dataBus.Vagons.Count-1; i++)
            {
                CinemachineVirtualCamera nextCamera = Instantiate(_firstCamera);
                nextCamera.transform.position += new Vector3(offset, 0f, 0f);
                offset -= 11f;
                _cameras.Add(nextCamera);
            }
        }

        private void Start()
        {
            _firstCamera.MoveToTopOfPrioritySubqueue();
            _currentCamIndex = 0;
            VagonChanged?.Invoke(_currentCamIndex);
        }

        public void MoveLeft()
        {
            if(_currentCamIndex+1 > _cameras.Count-1)return;
            _cameras[++_currentCamIndex].MoveToTopOfPrioritySubqueue();
            VagonChanged?.Invoke(_currentCamIndex);
        }

        public void MoveRight()
        {
            if(_currentCamIndex-1 < 0)return;
            _cameras[--_currentCamIndex].MoveToTopOfPrioritySubqueue();
            VagonChanged?.Invoke(_currentCamIndex);
        }
    }
}
