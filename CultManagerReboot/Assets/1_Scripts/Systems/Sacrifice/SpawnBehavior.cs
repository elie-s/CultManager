using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class SpawnBehavior : MonoBehaviour
    {
        public Spawn spawn;
        public DemonManager manager;
        private bool dead;


        private void Update()
        {
            if (spawn.CheckDeath() && !dead)
            {
                dead = true;
                manager.ReturnLoot(spawn);
                manager.KillSpawn(spawn);
            }
        }

        public void Init(Spawn _spawn,DemonManager _manager)
        {
            spawn = _spawn;
            manager = _manager;
        }
    }
}

