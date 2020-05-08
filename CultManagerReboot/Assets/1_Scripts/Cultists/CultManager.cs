using UnityEngine;


namespace CultManager
{
    public class CultManager : MonoBehaviour
    {
        //[SerializeField] private DebugInstance debug = default;

        [Header("Cult Parameters")]
        [SerializeField] private GameManager gameManager = default;
        [SerializeField] private CultistsDisplayer cultistsDisplayer = default;
        [SerializeField] private CultData data = default;
        [SerializeField] private int currentCandidatesDebug = 0;
        [SerializeField] private CultSettings settings = default;
        [SerializeField] Cultist[] cultists;

        [Header("Infiltration Mode")]
        public bool allowInfiltration;
        public int rateOfInfiltration;


        private bool useSave = false;

        private void Start()
        {
            cultistsDisplayer?.DisplayNewCultists(data.cultists.ToArray());
        }

        public void ResetCult(int level)
        {
            data.ResetCultData(level);
        }

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

            if (allowInfiltration)
            {
                result.isInvestigator = ChanceOfInfiltration();
                Debug.Log("aDDED "+result.isInvestigator);
                
            }

            return result;
        }

        public void AddCultists(params Cultist[] _cultists)
        {
            foreach (Cultist cultist in _cultists)
            {
                data.AddCultist(cultist);
            }

            if (useSave)
            {
                gameManager.SaveGame();
            }

            cultistsDisplayer?.DisplayNewCultists(_cultists);
        }

        public void ResetData()
        {
            data.Reset();
            //SetTestCultists(settings.testCultistsAmount);
        }

        public void DecreaseCandidates()
        {
            data.RemoveCandidateFromCount();
        }

        public void IncreaseCandidates()
        {
            data.AddCandidateToCount();
        }

        public void RemoveCutlist(Cultist _cultist)
        {
            data.RemoveCultist(_cultist);
        }

        public void GetToSacrifice()
        {
            cultistsDisplayer.DisplaySacrificeCultists(data.cultists.ToArray());
        }

        public void QuitSacrifice()
        {
            cultistsDisplayer.RemoveSacrificeCultists(data.cultists.ToArray());
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

        public bool ChanceOfInfiltration()
        {
            return ((int)Random.Range(1, 100) <= rateOfInfiltration);
        }
    }
}

