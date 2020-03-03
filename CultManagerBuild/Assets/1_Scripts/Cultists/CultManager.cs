using UnityEngine;

namespace CultManager
{
    public class CultManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private SaveManager saveManager = default;
        [SerializeField] private CultistSpawner spawner = default;
        [SerializeField] private CultData data = default;
        [SerializeField, DrawScriptable] private CultSettings settings = default;

        private bool useSave = false;

        private void Awake()
        {
            InitalizeData();
        }

        private void SetTestCultists(int _amount)
        {
            for (int i = 0; i < _amount; i++)
            {
                data.AddCultist(CreateRandomCultist());
            }
        }

        public Cultist CreateRandomCultist()
        {
            int sprite = Random.Range(0, settings.cultistThumbnails.Length);
            string cultistName = settings.cultistNames[Random.Range(0, settings.cultistNames.Length)];

            Cultist result = data.CreateCultist(cultistName, sprite);

            return result;
        }

        public void AddCultists(params Cultist[] _cultists)
        {
            foreach (Cultist cultist in _cultists)
            {
                data.AddCultist(cultist);
                spawner?.SpawnNewCultist();
            }

            if(useSave) saveManager.SaveGame();
        }

        private void InitalizeData()
        {
            useSave = saveManager;

            bool saveLoaded = useSave ? saveManager.Loadgame() : false;
            if (!saveLoaded && (data.cultists == null || data.cultists.Count == 0))
            {
                data.Reset();
                SetTestCultists(settings.testCultistsAmount);
            }
        }

        [ContextMenu("Reset Cult List")]
        private void ResetCultistList()
        {
            data.Reset();
        }

        [ContextMenu("Debug Cultist 0")]
        private void DebugCultist0()
        {
            Debug.Log(data.cultists[0]);
        }
    }
}