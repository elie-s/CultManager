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
        [SerializeField] private StatuesData statueData = default;
        [SerializeField] private DemonBookUI demonBook = default;
        [SerializeField] private EffectsManager effects = default;
        [SerializeField] private GameManager game = default;
        [SerializeField] private BoxCollider2D area = default;
        [SerializeField] private Transform spawnPosition = default;
        [SerializeField] private FadeBehavior worldFade = default;
        [SerializeField] private Transform[] waypoints = default;

        private List<SpawnBehavior> spawns = new List<SpawnBehavior>();



        private void Start()
        {
            /*InitSpawns();
            InitPersistent();*/
        }

        public void ResetData()
        {
            data.Reset();
            persistentData.Reset();
        }

        public void ResetSAPuzzle()
        {
            data.ResetSAPuzzle();
            persistentData.Reset();
        }

        public void ResetCult(int level)
        {
            data.ResetDemonData(level);
            KillAllSpawns();
            InitSpawns();
        }

        public void CreateNewDemon(int durationInHours, Segment[] segments,int patternSegments, int totalPatternSegments)
        {
            //reset.ActivateSpawn();
            Spawn spawn = data.CreateDemon(durationInHours, segments, effects.SetRandomSpawnEffect(segments.Length,patternSegments),patternSegments,totalPatternSegments, statueData.currentDemon);
            GameObject instance = Instantiate(spawnPrefab, spawnPosition.position, Quaternion.identity, transform);
            instance.GetComponent<SpawnBehavior>().Init(spawn, this,spawn.patternAccuracy,area,demonBook);
            spawns.Add(instance.GetComponent<SpawnBehavior>());
            spawns[spawns.Count - 1].spawnFade.ActivateFade();
            effects.UpdateModifiers();

        }

        public Spawn AddNewExperiment(int _durationInHours, Segment[] _segments, int _patternSegments, int _totalPatternSegments)
        {
            return data.CreateDemon(_durationInHours, _segments, new Modifier[0], _patternSegments, _totalPatternSegments, statueData.currentDemon);
        }

        public SpawnSummoningBehaviour SummonSpawn(Spawn _spawn)
        {
            SpawnSummoningBehaviour behaviour = Instantiate(spawnPrefab, spawnPosition.position, Quaternion.identity, transform).GetComponent<SpawnSummoningBehaviour>();
            behaviour.Init(_spawn);

            return behaviour;
        }

        public void CreateNewPersistentDemon(int spriteIndex)
        {
            //reset.ActivateDemon();
            worldFade.ActivateDelayedFade(3);
            PersistentDemon persistent = persistentData.CreatePersistentDemon(spriteIndex);
            GameObject instance = Instantiate(persistentDemonPrefab, spawnPosition.position, Quaternion.identity, transform);
            instance.GetComponent<PersistentDemonBehavior>().Init(persistent, this,area);
            instance.GetComponent<PersistentDemonBehavior>().spawnFade.ActivateFade();
            effects.UpdateModifiers();
        }

        void KillAllSpawns()
        {
            for (int i = 0; i < spawns.Count; i++)
            {
                KillSpawn(spawns[i].spawn);
            }
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
                    GameObject instance = Instantiate(spawnPrefab, spawnPosition.position, Quaternion.identity, transform);
                    instance.GetComponent<SpawnBehavior>().Init(data.spawns[i], this, data.spawns[i].patternAccuracy, area,demonBook);
                    spawns.Add(instance.GetComponent<SpawnBehavior>());
                    spawns[i].EnableMove();
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
                    GameObject instance = Instantiate(persistentDemonPrefab, spawnPosition.position, Quaternion.identity, transform);
                    instance.GetComponent<PersistentDemonBehavior>().Init(persistentData.persistentDemons[i], this,area);
                    instance.GetComponent<PersistentDemonBehavior>().EnableMove();
                    effects.UpdateModifiers();
                }
            }
        }

        private Vector2 GetRandomPos()
        {
            return waypoints[Random.Range(0, waypoints.Length)].position;
        }

        public void ResetPuzzle()
        {
            game.ResetCult(1);
        }
    }
}

