using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class PageBehavior : MonoBehaviour
    {
        public DemonBookUI demonBook;
        public int pageIndex;
        [SerializeField] private TMP_Text pageNumText;

        private void Start()
        {
            demonBook = FindObjectOfType<DemonBookUI>();
        }

        public void InitText(int i)
        {
            pageIndex = i;
            pageNumText.text = i.ToString();
        }
        public void PageClick()
        {
            demonBook.PageActive(pageIndex);
        }
    }
}

