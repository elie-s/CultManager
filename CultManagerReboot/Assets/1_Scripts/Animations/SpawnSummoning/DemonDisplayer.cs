using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class DemonDisplayer : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private GameObject[] demons = default;


        void Start()
        {
            DisplayDemons();
        }

        public void DisplayDemons()
        {
            for (int i = 0; i < 10; i++)
            {
                demons[i].SetActive(data.GetStatueSet(i).summoned);
            }
        }
    }
}