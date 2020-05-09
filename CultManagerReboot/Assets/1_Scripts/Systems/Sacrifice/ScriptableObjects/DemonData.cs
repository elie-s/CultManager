using CultManager.HexagonalGrid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demon/Data")]
    public class DemonData : ScriptableObject
    {
        public List<Demon> demons;
        public List<Spawn> spawns;
        public int idIndex = 0;
        public int spawnCount;


        public void Reset()
        {
            spawns = new List<Spawn>();
            demons = new List<Demon>();
            idIndex = 0;
            spawnCount = 0;
        }

        public void ResetDemonData(int level)
        {
            Reset();
        }


        public void LoadSave(Save _save)
        {
            spawns = _save.spawns.ToList();
            demons = _save.demons.ToList();
            idIndex = _save.demonIdIndex;
            spawnCount = _save.spawnCount;
        }

        public Spawn CreateDemon(int durationInHours, Segment[] segments,Modifier[] modifier)
        {
            Spawn spawn = new Spawn(idIndex, durationInHours,modifier);

            System.DateTime deathTime = System.DateTime.Now+System.TimeSpan.FromHours(durationInHours);
            Demon demon = new Demon(idIndex, segments, deathTime);

            AddDemon(demon);
            AddSpawn(spawn);

            idIndex++;
            return spawn;
        }

        public Demon ReturnDemonForSpawn(Spawn spawn)
        {
            Demon current = new Demon();
            for (int i = 0; i < demons.Count; i++)
            {
                if (demons[i].id == spawn.id)
                {
                    current = demons[i];
                }
            }
            return current;
        }


        public int ReturnLoot(int id)
        {
            int loot = 0;
            for (int i = 0; i < demons.Count; i++)
            {
                if (id == demons[i].id)
                {
                    loot = demons[i].lootBonus;
                }
            }
            return loot;
        }


        public void RemoveSpawn(Spawn spawn)
        {
            spawns.Remove(spawn);
            spawnCount--;
        }

        void AddSpawn(Spawn spawn)
        {
            spawns.Add(spawn);
            spawnCount++;
        }

        void AddDemon(Demon demon)
        {
            demons.Add(demon);
        }

        public void RemoveDemon(Demon demon)
        {
            demons.Remove(demon);
        }


        public void ToggleStar(int index)
        {
            demons[index].ToggleStar();
        }

    }
}

