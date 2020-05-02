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


        public Spawn(int _id,int _durationInHours)
        {
            id = _id;
            durationInHours = _durationInHours;
            ResetSpawnTime();
            Debug.Log(spawnTime);
        }

        public void ResetSpawnTime()
        {
            spawnTime = DateTime.Now;
        }

        public bool CheckDeath()
        {
            TimeSpan timeSpan = DateTime.Now - spawnTime;
            return (timeSpan.TotalMinutes > (durationInHours * 60));
        }

    }
}

