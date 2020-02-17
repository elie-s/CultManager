using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CultManager : MonoBehaviour
    {
        [SerializeField] private CultData data = default;
        [SerializeField, DrawScriptable] private CultSettings settings = default;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Cultist CreateRandomCultist()
        {
            Sprite sprite = settings.cultistThumbnails[Random.Range(0, settings.cultistThumbnails.Length)];
            string cultistName = settings.cultistNames[Random.Range(0, settings.cultistNames.Length)];

            return data.CreateCultist(cultistName, sprite);
        }
    }
}