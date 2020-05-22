using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public abstract class Gesture : MonoBehaviour
    {
        public static bool Touching { get; protected set; }
        public static bool BeginTouch { get; protected set; }
        public static bool QuickTouch { get; protected set; }
        public static bool LongTouch { get; protected set; }
        public static bool DoubleTouch { get; protected set; }
        public static bool ReleasedMovementTouch { get; protected set; }
        public static bool MultipleTouches { get; protected set; }
        public static Vector2 Movement { get; protected set; }
        public static Vector2 DeltaMovement { get; protected set; }
        public static float PinchValue { get; protected set; }
        public static float PinchDeltaValue { get; protected set; }
        public static bool Pinching { get; protected set; }
        public static Vector2 PinchMiddlePoint { get; protected set; }
        public static float RotationValue { get; protected set; }
        public static bool Rotating { get; protected set; }
    }
}