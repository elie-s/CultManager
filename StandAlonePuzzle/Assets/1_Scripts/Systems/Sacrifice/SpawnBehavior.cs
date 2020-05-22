using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class SpawnBehavior : MonoBehaviour
    {
        public Spawn spawn;
        public DemonManager manager;

        [SerializeField] private SpriteRenderer body;
        [SerializeField] private SpriteRenderer alpha;
        [SerializeField] private Gradient accuracyColor = default;
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

        public void Init(Spawn _spawn,DemonManager _manager,float accuracy)
        {
            spawn = _spawn;
            manager = _manager;
            ColorIt(accuracy);
        }

        public void ColorIt(float accuracy)
        {
            alpha.color = accuracyColor.Evaluate(accuracy);
        }
    }
}

