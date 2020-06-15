using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class DemonDisplayAction : MonoBehaviour
    {
        [SerializeField] private DemonBookUI demonBook = default;
        [SerializeField] private TMP_Text goodLinksSummaryText = default;
        [SerializeField] private TMP_Text spawnLinksSummaryText = default;
        public int index;

        private void Start()
        {
            demonBook = FindObjectOfType<DemonBookUI>();
            
        }

        public void Init(int goodLinks,int spawnLinks,int _Index)
        {
            goodLinksSummaryText.text = goodLinks.ToString();
            spawnLinksSummaryText.text = spawnLinks.ToString();
            index = _Index;
        }

        public void DisplayDemonBehavior()
        {
            demonBook.DisplayThisDemon(index);
        }
    }
}

