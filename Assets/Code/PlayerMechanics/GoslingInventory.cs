using System;
using Code.PlayerMechanics.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Code.PlayerMechanics
{
    public class GoslingInventory : MonoBehaviour
    {
        public event Action<HandheldTool> ActiveToolChanged;
        [SerializeField] private HandheldTool[] _tools;
        [SerializeField] private Image[] _icons;
        [SerializeField] private GoslingInput _input;
        private HandheldTool _currentTool;

        private void Start()
        {
            _currentTool = _tools[0];
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1)) EquipTool(_tools[0]);
            if(Input.GetKeyDown(KeyCode.Alpha2)) EquipTool(_tools[1]);
            if(Input.GetKeyDown(KeyCode.Alpha3)) EquipTool(_tools[2]);
            if(Input.GetKeyDown(KeyCode.Alpha4)) EquipTool(_tools[3]);
            
            if (Input.GetMouseButtonDown(0))
            {
                _currentTool.Use(_input.GetLookDirection());
            }
        }

        private void EquipTool(HandheldTool nextTool)
        {
            if(nextTool == _currentTool)return;
            _currentTool.gameObject.SetActive(false);
            _currentTool = nextTool;
            _currentTool.gameObject.SetActive(true);
            ActiveToolChanged?.Invoke(_currentTool);
            foreach (Image image in _icons)
            {
                image.color = new Color(1f, 1f, 1f, 0.5f);
            }

            _icons[Array.IndexOf(_tools, nextTool)].color = Color.white;
        }
    }
}
