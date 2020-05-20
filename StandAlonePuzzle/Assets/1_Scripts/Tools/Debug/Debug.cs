using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class DebugInstance
    {
        public Importance filter;

        public DebugInstance()
        {
            filter = (Importance)15;
        }

        public void Log(object _object, Importance _importance)
        {
            if (filter.HasFlag(_importance)) Debug.Log(_object);
        }

        public void LogWarning(object _object, Importance _importance)
        {
            if (filter.HasFlag(_importance)) Debug.LogWarning(_object);
        }

        public void LogError(object _object, Importance _importance)
        {
            if (filter.HasFlag(_importance)) Debug.LogError(_object);
        }

        [System.Flags]
        public enum Importance { none = 0,  Lesser = 1, Average = 2, Highest = 4, Mandatory = 8}
    }
}