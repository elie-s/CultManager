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
            
        }

        public void Init(int goodLinks,int spawnLinks,int _pageIndex,int _numberOfItemsPerPage)
        {
            goodLinksSummaryText.text = goodLinks.ToString();
            spawnLinksSummaryText.text = spawnLinks.ToString();
            index = transform.GetSiblingIndex()+(_pageIndex*_numberOfItemsPerPage);
        }

        public void DisplayDemonBehavior()
        {
            demonBook.DisplayThisDemon(index);
        }
    }
}

