using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class DemonsShowed : MonoBehaviour
    {
        [SerializeField] private StatuesData data = default;
        [SerializeField] private GameObject[] demons = default;

        private void OnEnable()
        {
            demons[(int)(data.currentDemon) - 1].SetActive(true);
        }
    }
}