using System;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [Serializable]
    public struct GesturesDataRecord
    {
        public bool beginTouch;
        public bool quickTouch;
        public bool longTouch;
        public bool doubleTouch;
        public bool releasedMovementTouch;
        public bool multipleTouches;
        public float movementX;
        public float movementY;
        public float deltaX;
        public float deltaY;
        public float pinchValue;
        public float pinchDeltaValue;
        public bool pinching;
        public float rotationValue;
        public bool rotating;
        public float timeCode;
        public DateTime time;

        public static GesturesDataRecord current => new GesturesDataRecord()
        {
            beginTouch = Gesture.BeginTouch,
            quickTouch = Gesture.QuickTouch,
            longTouch = Gesture.LongTouch,
            doubleTouch = Gesture.DoubleTouch,
            releasedMovementTouch = Gesture.ReleasedMovementTouch,
            multipleTouches = Gesture.MultipleTouches,
            movementX = Gesture.Movement.x,
            movementY = Gesture.Movement.y,
            deltaX = Gesture.DeltaMovement.x,
            deltaY = Gesture.DeltaMovement.y,
            pinchValue = Gesture.PinchValue,
            pinchDeltaValue = Gesture.PinchDeltaValue,
            pinching = Gesture.Pinching,
            rotationValue = Gesture.RotationValue,
            rotating = Gesture.Rotating,
            timeCode = Time.time,
            time = DateTime.Now
        };

        public override string ToString()
        {
            return (beginTouch ? 1 : 0) + DataRecorder.Separation +
                    (quickTouch ? 1 : 0) + DataRecorder.Separation +
                    (longTouch ? 1 : 0) + DataRecorder.Separation +
                    (doubleTouch ? 1 : 0) + DataRecorder.Separation +
                    (releasedMovementTouch ? 1 : 0) + DataRecorder.Separation +
                    (multipleTouches ? 1 : 0) + DataRecorder.Separation +
                    movementX + DataRecorder.Separation +
                    movementY + DataRecorder.Separation +
                    deltaX + DataRecorder.Separation +
                    deltaY + DataRecorder.Separation +
                    pinchValue + DataRecorder.Separation +
                    pinchDeltaValue + DataRecorder.Separation +
                    (pinching ? 1 : 0) + DataRecorder.Separation +
                    rotationValue + DataRecorder.Separation +
                    (rotating ? 1 : 0) + DataRecorder.Separation +
                    time + DataRecorder.Separation +
                    timeCode;
        }
    }

    [Serializable]
    public struct GestureRecord
    {
        public GesturesDataRecord[] data;

        public GestureRecord(GesturesDataRecord[] _data)
        {
            data = _data;
        }

        public GestureRecord(List<GesturesDataRecord> _data)
        {
            data = _data.ToArray();
        }

        public string[] DataToString(int _id)
        {
            List<string> result = new List<string>();

            foreach (GesturesDataRecord dataRecord in data)
            {
                result.Add(_id + DataRecorder.Separation + dataRecord.ToString());
            }

            return result.ToArray();
        }
    }
}

