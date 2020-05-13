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
        [SerializeField] private CultManager cult = default;
        [SerializeField] private AltarManager altar = default;

        [SerializeField] private EffectsManager effects;

        private List<SpawnBehavior> spawns=new List<SpawnBehavior>();



        private void Start()
        {
            InitSpawns();
            InitPersistent();
        }

        public void ResetData()
        {
            data.Reset();
            persistentData.Reset();
        }

        public void ResetCult(int level)
        {
            data.ResetDemonData(level);
        }

        public void CreateNewDemon(int durationInHours, Segment[] segments,int patternSegments)
        {
            Spawn spawn = data.CreateDemon(durationInHours, segments, effects.SetRandomSpawnEffect(segments.Length,patternSegments),patternSegments);
            GameObject instance = Instantiate(spawnPrefab, transform.position, Quaternion.identity, transform);
            instance.GetComponent<SpawnBehavior>().Init(spawn, this,spawn.patternAccuracy);
            spawns.Add(instance.GetComponent<SpawnBehavior>());
            effects.UpdateModifiers();
        }

        public void CreateNewPersistentDemon(int spriteIndex)
        {
            PersistentDemon persistent = persistentData.CreatePersistentDemon(spriteIndex);
            GameObject instance = Instantiate(persistentDemonPrefab, transform.position, Quaternion.identity, transform);
            instance.GetComponent<PersistentDemonBehavior>().Init(persistent, this);
            effects.UpdateModifiers();

            Invoke("ResetCultProgress", 10f);

        }

        public void BreakRandomAltarPart()
        {
            altar.BreakAltarPart();
        }

        [ContextMenu("Reset Progress")]
        void ResetCultProgress()
        {
            cult.ResetCultProgress();
        }

        public void KillSpawn(Spawn spawn)
        {
            data.RemoveSpawn(spawn);
            KillSpawnInstance(spawn);
            effects.UpdateModifiers();
        }

        public void ReturnLoot(Spawn spawn)
        {
            data.ReturnLoot(spawn.id);
        }

        public void KillSpawnInstance(Spawn spawn)
        {
            SpawnBehavior current;
            data.RemoveSpawn(spawn);
            for (int i = 0; i < spawns.Count; i++)
            {
                if (spawns[i].spawn == spawn)
                {
                    current = spawns[i];
                    Destroy(spawns[i].gameObject);
                    spawns.Remove(spawns[i]);
                    break;
                }
            }
        }


        public void InitSpawns()
        {
            if (data.spawnCount > 0)
            {
                for (int i = 0; i < data.spawns.Count; i++)
                {
                    GameObject instance = Instantiate(spawnPrefab, transform.position, Quaternion.identity, transform);
                    instance.GetComponent<SpawnBehavior>().Init(data.spawns[i], this, data.spawns[i].patternAccuracy);
                    spawns.Add(instance.GetComponent<SpawnBehavior>());
                    effects.UpdateModifiers();
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
                    effects.UpdateModifiers();
                }
            }
        }
    }
}

