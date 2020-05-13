using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace CultManager
{
    [System.Serializable]
    public class Spawn
    {
        public int id;
        public DateTime spawnTime;
        public int durationInHours;
        public float patternAccuracy;

        public Modifier[] modifiers;


        public Spawn(int _id,int _durationInHours,Modifier[] _modifiers,float _patternAccuracy)
        {
            id = _id;
            durationInHours = _durationInHours;
            modifiers = _modifiers;
            patternAccuracy = _patternAccuracy;
            ResetSpawnTime();
        }

        public void ResetSpawnTime()
        {
            spawnTime = DateTime.Now;
        }

        public bool CheckDeath()
        {
            TimeSpan timeSpan = DateTime.Now - spawnTime;
            return (timeSpan.Minutes >= (durationInHours*60));
            
        }

    }
}

