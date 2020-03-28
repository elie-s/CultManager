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
    public float percentage => ratio * 100;

    public Gauge(float _min, float _max, bool _startFull = true)
    {
        min = _min;
        max = _max;
        value = _startFull ? _max : _min;
        Debug.Log("Value "+value+", Min "+min+ ", Max " + max);
    }

    public void SetValue(float _value)
    {
        value = Mathf.Clamp(_value, min, max);
    }

    public void Increment(float _increment)
    {
        value = Mathf.Clamp(value + _increment, min, max);
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
        value = Mathf.Clamp(_value, min, max);
    }

    public void Increment(int _increment)
    {
        value = Mathf.Clamp(value + _increment, min, max);
    }
}
