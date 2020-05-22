using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class FloatingBehaviour : MonoBehaviour
    {
        [SerializeField] private bool playOnStart = default;
        [SerializeField] private bool loop = default;
        [SerializeField] private bool playLocal = default;
        [SerializeField] private Transform floatingObject = default;
        [SerializeField, DrawScriptable] private FloatingSettings settings;

        private Transform transformToUse => floatingObject ? floatingObject : transform;

        private Vector2 startPos;

        private void Start()
        {
            startPos = transform.position;

            if(playOnStart) Float();
        }

        public void Float()
        {
            StartCoroutine(FloatRoutine());
        }

        private IEnumerator FloatRoutine()
        {
            yield return playLocal ? settings.PlayLocal(transformToUse) : settings.Play(transformToUse);

            if (loop) StartCoroutine(FloatRoutine());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;

            if(Application.isPlaying) Gizmos.DrawLine(startPos, startPos + Vector2.up * settings.span);
            else Gizmos.DrawLine(transform.position, transform.position + Vector3.up * settings.span);
        }

    }
}