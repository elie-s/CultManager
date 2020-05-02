using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PoliceManager : MonoBehaviour
    {
        public PoliceData data;
        public void Incerment(int _value)
        {
            data.Increment(_value);
        }

        public void Decrement(int _value)
        {
            data.Decrement(_value);
        }

        public void Set(int _value)
        {
            data.Set(_value);
        }

        public void InitializeData()
        {
            if (!SaveManager.saveLoaded)
            {
                data.Reset(100);
            } 
        }

        public void ResetCult(int level)
        {
            int max = 100;
            data.ResetPoliceData(max);
        }
    }
}

