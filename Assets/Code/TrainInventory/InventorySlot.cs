using UnityEngine;
using UnityEngine.UI;

namespace Code.TrainInventory
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private void Awake()
        {
            ClearSlot();
        }

        public void FillSlot()
        {
            _image.color = Color.red;
        }

        public void ClearSlot()
        {
            _image.color = Color.green;
        }
    }
}