using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class PersistentDemonBehavior : MonoBehaviour
    {
        public PersistentDemon persistent;
        public DemonManager manager;


        private void Update()
        {
        }

        public void Init(PersistentDemon _persistent, DemonManager _manager)
        {
            persistent = _persistent;
            manager = _manager;
        }
    }
}

