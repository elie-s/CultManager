﻿using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class Cultist
    {
        public ulong id { get; private set; }
        public string cultistName { get; private set; }
        public int age { get; private set; }
        public int spriteIndex { get; private set; }
        public bool working { get; private set; }
        public bool isInvestigator { get; private set; }
        public bool detected { get; private set; }
        public CultistTraits traits { get; private set; }
        public BloodType blood { get; private set; }

        public Cultist(ulong _id, string _cultistName, int _spriteIndex)
        {
            id = _id;
            cultistName = _cultistName;
            spriteIndex = _spriteIndex;
            RandomAge();
            RandomTraits();
            RandomBloodType();
            working = false;
        }

        public void ToggleOccupy()
        {
            working = !working;
        }

        public void SetWorking(bool _value)
        {
            working = _value;
        }

        public void SetInvestigator(bool _value)
        {
            isInvestigator = _value;
        }

        public void SetDetected(bool _value)
        {
            detected = _value;
        }

        public void RandomAge()
        {
            int value = 11;

            for (int i = 0; i < 7; i++)
            {
                value += Random.Range(1, 7);
            }

            age = value;
        }

        public void RandomBloodType()
        {
            blood = (BloodType)Mathf.RoundToInt(Random.Range(0, 3));
        }

        public void RandomTraits()
        {
            traits = (CultistTraits)Random.Range(0, 65);
        }

    }
}

