using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PoliceManager : MonoBehaviour
    {
        public PoliceData data;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Incerment(int _value)
        {
            data.Increment(_value);
        }

        public void InitializeData()
        {
            if (!SaveManager.saveLoaded) data.Reset(100);
        }
    }
}