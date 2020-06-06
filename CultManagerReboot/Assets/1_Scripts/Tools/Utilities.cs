using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static string Format(this long _value)
    {
        if (_value > 9999999999) return Mathf.RoundToInt((float)_value / (float)1000000).ToString() + "B";
        else if (_value > 9999999) return Mathf.RoundToInt((float)_value / (float)1000000).ToString() + "M";
        else if (_value > 9999) return Mathf.RoundToInt((float)_value / (float)1000).ToString() + "K";

        return _value.ToString();
    }

    public static string Format(this uint _value)
    {
        if (_value > 9999999) return Mathf.RoundToInt((float)_value / (float)1000000).ToString() + "M";
        else if (_value > 9999) return Mathf.RoundToInt((float)_value / (float)1000).ToString() + "K";

        return _value.ToString();
    }

    public static string Format(this int _value)
    {
        if (_value > 9999999) return Mathf.RoundToInt((float)_value / (float)1000000).ToString() + "M";
        else if (_value > 9999) return Mathf.RoundToInt((float)_value / (float)1000).ToString() + "K";

        return _value.ToString();
    }
}
