using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class DemonDisplayAction : MonoBehaviour
    {
        [SerializeField] private DemonBookUI demonBook;
        [SerializeField] private TMP_Text goodLinksSummaryText;
        [SerializeField] private TMP_Text spawnLinksSummaryText;
        public int index;

        private void Start()
        {
            demonBook = FindObjectOfType<DemonBookUI>();
            index = transform.GetSiblingIndex();
        }

        public void Init(int goodLinks,int spawnLinks)
        {
            goodLinksSummaryText.text = goodLinks.ToString();
            spawnLinksSummaryText.text = spawnLinks.ToString();
        }

        public void DisplayDemonBehavior()
        {
            demonBook.DisplayThisDemon(index);
        }
    }
}

