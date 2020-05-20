using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class DemonDisplayAction : MonoBehaviour
    {
        [SerializeField] private DemonBookUI demonBook;
        public int index;

        private void Start()
        {
            demonBook = FindObjectOfType<DemonBookUI>();
            index = transform.GetSiblingIndex();
        }

        public void DisplayDemonBehavior()
        {
            Debug.Log("Press");
            demonBook.DisplayThisDemon(index);
        }
    }
}

