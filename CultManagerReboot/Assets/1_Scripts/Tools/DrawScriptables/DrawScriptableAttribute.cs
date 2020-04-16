using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScriptableAttribute : PropertyAttribute
{
    public static Dictionary<string, bool> hide;

    public DrawScriptableAttribute()
    {
        if(hide == null) hide = new Dictionary<string, bool>();
    }
}
