using System.Collections;
using CultManager.HexagonalGrid;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class DemonManager : MonoBehaviour
    {
        [SerializeField] private DemonData data = default;
        [SerializeField] private PersistentDemonData persistentData = default;
        [SerializeField] private GameObject spawnPrefab = default;
        [SerializeField] private GameObject persistentDemonPrefab = default;
        [SerializeField] private MoneyManager money = default;



        private void Start()
        {
            if (!SaveManager.saveLoaded)
            {
                data.Reset();
                persistentData.Reset();
            }
            InitSpawns();
        }

        public void ResetCult(int level)
        {
            data.ResetDemonData(level);
        }

        public void CreateNewDemon(int durationInHours, Segment[] segments)
        {
            Spawn spawn=data.CreateDemon(durationInHours, segments);
            GameObject instance = Instantiate(spawnPrefab, transform.position, Quaternion.identity, transform);
            instance.GetComponent<SpawnBehavior>().Init(spawn,this);
        }

        public void CreateNewPersistentDemon(int spriteIndex)
        {
            PersistentDemon persistent = persistentData.CreatePersistentDemon(spriteIndex);
            GameObject instance = Instantiate(persistentDemonPrefab, transform.position, Quaternion.identity, transform);
            instance.GetComponent<PersistentDemonBehavior>().Init(persistent, this);

        }

        public void KillSpawn(Spawn spawn)
        {
            data.RemoveSpawn(spawn);
        }

        public void ReturnLoot(Spawn spawn)
        {
            data.ReturnLoot(spawn.id);
        }


        public void InitSpawns()
        {
            if (data.spawnCount > 0)
            {
                for (int i = 0; i < data.spawns.Count; i++)
                {
                    GameObject instance = Instantiate(spawnPrefab, transform.position, Quaternion.identity, transform);
                    instance.GetComponent<SpawnBehavior>().Init(data.spawns[i], this);
                }
            }
        }

        public void InitPersistent()
        {
            if (persistentData.persistentDemons.Count > 0)
            {
                for (int i = 0; i < persistentData.persistentDemons.Count; i++)
                {
                    GameObject instance = Instantiate(persistentDemonPrefab, transform.position, Quaternion.identity, transform);
                    instance.GetComponent<PersistentDemonBehavior>().Init(persistentData.persistentDemons[i], this);
                }
            }
        }
    }
}

