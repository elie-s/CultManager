using System.Collections;
using CultManager.HexagonalGrid;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class DemonManager : MonoBehaviour
    {
        [SerializeField]private DemonData data=default;


        private void Start()
        {
            if (!SaveManager.saveLoaded)
            {
                data.Reset();
            }
        }

        public void CreateNewDemon(int durationInHours, Segment[] segments)
        {
            data.CreateDemon(durationInHours, segments);
        }

    }
}

