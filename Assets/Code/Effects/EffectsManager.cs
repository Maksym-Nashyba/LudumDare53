using Code.PlayerMechanics;
using UnityEngine;

namespace Code
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private UnityEngine.GameObject _shotPrefab;

        public void PlayShotEffect(Vector3 origin, Vector3 target)
        {
            ShotEffect shotInstance = Instantiate(_shotPrefab).GetComponent<ShotEffect>();
            shotInstance.Play(origin, target, 1f);
        }
        
        public void PlayShotEffectNoTarget(Vector3 origin, Vector3 direction)
        {
            PlayShotEffect(origin, origin + (direction.normalized * 25f));
        }
    }
}
