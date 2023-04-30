using UnityEngine;

namespace Code.ActionPhase
{
    public class Healthbar : MonoBehaviour
    {
        public void UpdateBar(float healthFraction)
        {
            transform.localScale = new Vector3(11 * healthFraction, 0.68f, 1f);
        }
    }
}
