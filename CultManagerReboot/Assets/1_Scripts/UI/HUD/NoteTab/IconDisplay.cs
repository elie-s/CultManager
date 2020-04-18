using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CultManager
{
    public class IconDisplay : MonoBehaviour
    {
        [SerializeField] private NoteTabSettings settings;
        [SerializeField] private GameObject[] iconObjects;

        private void Start()
        {
            Display();
        }

        private void Display()
        {
            for (int i = 0; i < iconObjects.Length; i++)
            {
                Image image = iconObjects[i].GetComponent<Image>();
                image.sprite = settings.icons[i];
            }
        }
    }
}

