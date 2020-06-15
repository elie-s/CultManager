using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Gestures/SwipeTrigger")]
    public class SwipeTrigger : ScriptableObject
    {
        public Vector2 direction = Vector2.right;
        public float spanAngle = 45.0f;
        public float distanceThreshold = 0.25f;

        public Coroutine Play(MonoBehaviour _behaviour, System.Action _callback)
        {
            return _behaviour.StartCoroutine(DetectionRoutine(_callback));
        }

        private IEnumerator DetectionRoutine(System.Action _callback)
        {
            while (true)
            {
                while (Verify())
                {
                    yield return null;
                }

                while (!Gesture.ReleasedMovementTouch)
                {
                    yield return null;
                }

                if (Gesture.Movement.magnitude >= distanceThreshold && Vector2.Angle(Gesture.Movement, direction) < spanAngle) _callback();

                yield return null;

            }
        }

        private bool Verify()
        {
            return !Gesture.MultipleTouches
                && Gesture.Movement != Vector2.zero
                && Vector2.Angle(Gesture.Movement, direction) < spanAngle * 2
                && Gesture.Movement.magnitude < distanceThreshold;
                
        }
    }
}