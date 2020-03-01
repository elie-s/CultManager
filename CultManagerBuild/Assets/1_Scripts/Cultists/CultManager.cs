using UnityEngine;

namespace CultManager
{
    public class CultManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private CultData data = default;
        [SerializeField, DrawScriptable] private CultSettings settings = default;

        private void Awake()
        {
            if (data.cultists == null || data.cultists.Count == 0) SetTestCultists(settings.testCultistsAmount);
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