using UnityEngine;


namespace CultManager
{
    public class CultManager : MonoBehaviour
    {
        //[SerializeField] private DebugInstance debug = default;
        [SerializeField] private GameManager gameManager = default;
        //[SerializeField] private CultistSpawner spawner = default;
        [SerializeField] private CultData data = default;
        [SerializeField] private int currentCandidatesDebug = 0;
        [SerializeField] private CultSettings settings = default;
        [SerializeField] Cultist[] cultists;


        private bool useSave = false;

        void Update()
        {
            currentCandidatesDebug = data.candidatesCount;
            cultists = data.cultists.ToArray();
        }

        void SetTestCultists(int _amount)
        {
            for (int i = 0; i < _amount; i++)
            {
                data.AddCultist(CreateRandomCultist());
            }
        }

        public Cultist CreateRandomCultist()
        {
            int sprite = Random.Range(0, settings.cultistThumbnails.Length);
            string cultistName = settings.cultistNames[Random.Range(0, settings.cultistNames.Length)] + " " + settings.cultistLastNames[Random.Range(0, settings.cultistLastNames.Length)];

            Cultist result = data.CreateCultist(cultistName, sprite);

            return result;
        }

        public void AddCultists(params Cultist[] _cultists)
        {
            foreach (Cultist cultist in _cultists)
            {
                data.AddCultist(cultist);
                //spawner?.SpawnNewCultist();
            }

            if (useSave)
            {
                gameManager.SaveGame();
            }
        }

        public void InitalizeData()
        {
            useSave = SaveManager.saveLoaded;

            if (!useSave && (data.cultists == null || data.cultists.Count == 0))
            {
                data.Reset();
                SetTestCultists(settings.testCultistsAmount);
            }
        }

        public void DecreaseCandidates()
        {
            data.RemoveCandidateFromCount();
        }

        public void IncreaseCandidates()
        {
            data.AddCandidateToCount();
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

