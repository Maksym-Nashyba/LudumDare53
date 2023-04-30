using System.Collections;
using Code.PlayerMechanics;
using UnityEngine;

namespace Code.Effects
{
    [RequireComponent(typeof(LineRenderer))]
    public class ShotEffect : Effect
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private AnimationCurve _widthOverLifetime;
        
        public void Play(Vector3 origin, Vector3 target, float duration)
        {
            _line.SetPosition(0, origin);
            _line.SetPosition(1, target);
            _line.startWidth = _line.endWidth = 0f;
            StartCoroutine(PlayOverTime(duration));
        }

        private IEnumerator PlayOverTime(float duration)
        {
            float passed = 0f;
            while (passed < duration)
            {
                _line.SetPosition(0, _line.GetPosition(0) + new Vector3(-Time.deltaTime * 2.5f, 0f, 0f));
                _line.SetPosition(1, _line.GetPosition(1) + new Vector3(-Time.deltaTime * 2.5f, 0f, 0f));
                _line.startWidth = _line.endWidth = _widthOverLifetime.Evaluate(passed / duration);
                yield return null;
                passed += Time.deltaTime;
            }
            Destroy(gameObject);
        }
    }
}