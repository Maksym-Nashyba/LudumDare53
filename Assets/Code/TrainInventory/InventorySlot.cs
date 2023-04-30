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

        public void PaintFilled()
        {
            _image.color = Color.red;
        }
        
        public void PaitTemp()
        {
            _image.color = Color.magenta;
        }

        public void ClearSlot()
        {
            _image.color = Color.green;
        }

        public bool IsInSlot(Vector3 position)
        {
            return (transform.position - position).magnitude <= 0.5f;
        }
    }
}