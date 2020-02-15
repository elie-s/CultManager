using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager.Test
{
    public class CultistCountManager_Test : MonoBehaviour
    {


        private void Recruitment(int _duration, int _frequency, int[] _recruitersId)
        {
            int recruitersCount = _recruitersId.Length;
            int iterations = _duration * 60 / _frequency;
            System.DateTime dateTime = System.DateTime.Now;

            for (int i = 0; i < iterations; i++)
            {

            }
        }
    }
}