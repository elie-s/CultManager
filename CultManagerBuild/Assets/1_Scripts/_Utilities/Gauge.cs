using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gauge
{
    public float min { get; private set; }
    public float max { get; private set; }
    public float value { get; private set; }
    public float ratio => (value - min) / (max - min);

    public Gauge(float _min, float _max, bool _startFull = true)
    {
        min = _min;
        max = _max;
        value = _startFull ? _max : _min;
    }

    public void SetValue(float _value)
    {
        value = Mathf.Clamp(min, max, _value);
    }

    public void Increment(float _increment)
    {
        value = Mathf.Clamp(min, max, value + _increment);
    }
}

[System.Serializable]
public class IntGauge
{
    public int min { get; private set; }
    public int max { get; private set; }
    public int value { get; private set; }
    public int ratio => (value - min) / (max - min);

    public IntGauge(int _min, int _max, bool _startFull = true)
    {
        min = _min;
        max = _max;
        value = _startFull ? _max : _min;
    }

    public void SetValue(int _value)
    {
        value = Mathf.Clamp(min, max, _value);
    }

    public void Increment(int _increment)
    {
        value = Mathf.Clamp(min, max, value + _increment);
    }
}
