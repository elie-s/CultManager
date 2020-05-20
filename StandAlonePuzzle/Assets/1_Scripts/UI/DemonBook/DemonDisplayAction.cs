using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class DemonDisplayAction : MonoBehaviour
    {
        [SerializeField] private DemonBookUI demonBook;
        [SerializeField] private TMP_Text accuracy;
        public int index;

        private void Start()
        {
            demonBook = FindObjectOfType<DemonBookUI>();
            index = transform.GetSiblingIndex();
        }

        public void Init(string description)
        {
            accuracy.text = description;
        }

        public void DisplayDemonBehavior()
        {
            Debug.Log("Press");
            demonBook.DisplayThisDemon(index);
        }
    }
}

