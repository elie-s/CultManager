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

        public Vector2 DemonPosition(DemonName _demon)
        {
            demons[(int)_demon].SetActive(true);
            return demons[(int)_demon].transform.position;
        }
    }
}