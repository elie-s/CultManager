using System;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [Serializable]
    public struct TouchData
    {
        public int id;
        public float posX;
        public float posY;
        public float deltaX;
        public float deltaY;
        public int phase;
        public float pressure;
        public float timeCode;
        public float deltaTime;
        public DateTime time;

        public TouchData(Touch _touch)
        {
            id = _touch.fingerId;
            posX = _touch.position.x;
            posY = _touch.position.y;
            deltaX = _touch.deltaPosition.x;
            deltaY = _touch.deltaPosition.y;
            phase = (int)_touch.phase;
            pressure = _touch.pressure;
            timeCode = Time.time;
            deltaTime = _touch.deltaTime;
            time = DateTime.Now;
        }

        public override string ToString()
        {
            return id + DataRecorder.Separation +
                    posX + DataRecorder.Separation +
                    posY + DataRecorder.Separation +
                    deltaX + DataRecorder.Separation +
                    deltaY + DataRecorder.Separation +
                    phase + DataRecorder.Separation +
                    pressure + DataRecorder.Separation +
                    deltaTime + DataRecorder.Separation +
                    time + DataRecorder.Separation +
                    timeCode + DataRecorder.Separation;
        }
    }

    [Serializable]
    public struct TouchRecord
    {
        public TouchData[] data;

        public TouchRecord(TouchData[] _data)
        {
            data = _data;
        }

        public TouchRecord(List<TouchData> _data)
        {
            data = _data.ToArray();
        }

        public string[] DataToString(int _id)
        {
            List<string> result = new List<string>();

            foreach (TouchData dataRecord in data)
            {
                result.Add(_id + DataRecorder.Separation + dataRecord.ToString());
            }

            return result.ToArray();
        }
    }

}
