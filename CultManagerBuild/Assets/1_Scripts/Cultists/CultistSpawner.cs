using UnityEngine;

namespace CultManager
{
    public class CultistSpawner : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private CultData data = default;
        [SerializeField] private GameObject cultistPrefab = default;
        [SerializeField] private float yValue = 0.4f;
        [SerializeField] private Vector2 spawnAreaMultipliers = new Vector2(4.0f, 3.0f);
        [SerializeField] private Transform parent = default;

        private void Start()
        {
            SpawnAllCultists();
        }

        private void SpawnAllCultists()
        {
            if (data.cultists == null || data.cultists.Count == 0)
            {
                debug.LogWarning("No cultist to spawn.", DebugInstance.Importance.Average);

                return;
            }

            foreach (Cultist cultist in data.cultists)
            {
                SpawnNewCultist();

                debug.Log(cultist + " spawned.", DebugInstance.Importance.Lesser);
            }

            debug.Log(data.cultists.Count + " cultists spawned.", DebugInstance.Importance.Average);
        }

        public void SpawnNewCultist()
        {
            Instantiate(cultistPrefab, RandomPosition(), Quaternion.identity, parent);
        }

        private Vector3 RandomPosition()
        {
            Vector2 pos = Random.insideUnitCircle;

            return new Vector3(pos.x * spawnAreaMultipliers.x, yValue, pos.y * spawnAreaMultipliers.y);
        }
    }
}