using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gauge
{
    [SerializeField] private float _value;
    [SerializeField] private float _max;

    public float min { get; private set; }
    public float max => _max;
    public float value => _value;
    public float ratio => (value - min) / (max - min);
    public float percentage => ratio * 100;
    public bool isFull => ratio == 1.0f;

    public Gauge(float _min, float _max, bool _startFull = true)
    {
        min = _min;
        this._max = _max;
        _value = _startFull ? _max : _min;
        Debug.Log("Value "+value+", Min "+min+ ", Max " + max);
    }

    public void SetValue()
    {
        _value = Mathf.Clamp(min, min, max);
    }

    public void Reset()
    {
        _value = min;
    }

    public void SetValue(float _value)
    {
        this._value = Mathf.Clamp(_value, min, max);
    }

    public void SetMax(int _max)
    {
        this._max = _max;
    }

    public void Increment(float _increment)
    {
        _value = Mathf.Clamp(value + _increment, min, max);
    }

    public void Decrement(float _decrement)
    {
        _value = Mathf.Clamp(value - _decrement, min, max);
    }
}

[System.Serializable]
public class IntGauge
{
    [SerializeField] private int _value;
    [SerializeField] private int _max;

    public int min { get; private set; }
    public int max => _max;
    public int value => _value;
    public float ratio =>(float)(value - min) / (float)(max - min);
    public int amountLeft => max - value;
    public bool isFull => max == value;

    public IntGauge(int _min, int _max, bool _startFull = true)
    {
        min = _min;
        this._max = _max;
        _value = _startFull ? _max : _min;
    }

    public IntGauge(int _min, int _max, int _val)
    {
        min = _min;
        this._max = _max;
        _value = _val;
    }

    public IntGauge(IntGauge intGauge)
    {
        min = intGauge.min;
        _max = intGauge.max;
        _value = intGauge.value;
    }

    public void SetValue()
    {
        _value = Mathf.Clamp(min, min, max);
    }

    public void Reset()
    {
        _value = min;
    }

    public void SetValue(int _value)
    {
        _value = Mathf.Clamp(_value, min, max);
    }

    public void SetMax(int _max)
    {
        this._max = _max;
    }

    public void Increment(int _increment)
    {
        _value = Mathf.Clamp(value + _increment, min, max);
    }

    public void Decrement(int _decrement)
    {
        _value = Mathf.Clamp(value - _decrement, min, max);
    }

    public override string ToString()
    {
        return value + "/" + max;
    }
}
