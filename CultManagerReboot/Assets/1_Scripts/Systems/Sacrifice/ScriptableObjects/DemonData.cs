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


        public void LoadSave(Save _save)
        {
            spawns = _save.spawns.ToList();
            demons = _save.demons.ToList();
            idIndex = _save.demonIdIndex;
            spawnCount = _save.spawnCount;
        }

        public void CreateDemon(int durationInHours, Segment[] segments)
        {
            Spawn spawn = new Spawn(idIndex, durationInHours);
            Demon demon = new Demon(idIndex, segments);

            AddDemon(demon);
            AddSpawn(spawn);

            spawnCount++;
            idIndex++;
        }


        public void RemoveSpawn(Spawn spawn)
        {
            spawns.Remove(spawn);
        }

        public void AddSpawn(Spawn spawn)
        {
            spawns.Add(spawn);
        }

        public void AddDemon(Demon demon)
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

