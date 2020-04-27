using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class DemonBookUI : MonoBehaviour
    {
        [Header("Display")]
        [SerializeField] private GameObject panel;
        [SerializeField] private Image demonImage;
        [SerializeField] private Image starImage;
        [SerializeField] private DemonData data;
        [SerializeField] private CurrentPanel thisPanelName;

        [Header("Display Sprites")]
        [SerializeField] private Sprite starActive;
        [SerializeField] private Sprite starInActive;

        [SerializeField]private int currentIndex = 0;

        public void Left()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = data.demons.Length;
            }
        }

        public void Right()
        {
            if (currentIndex < data.demons.Length-1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }

        private void Update()
        {
            DisplayCurrent();
        }

        public void DisplayCurrent()
        {
            if (data.demons[currentIndex].isStarred)
            {
                starImage.sprite = starActive;
            }
            else
            {
                starImage.sprite = starInActive;
            }
        }

        public void ToggleStar()
        {
            data.demons[currentIndex].ToggleStar();
        }
        [ContextMenu("Open")]
        public void Open()
        {
            if (GameManager.currentPanel == CurrentPanel.None)
            {
                GameManager.currentPanel = thisPanelName;
                panel.SetActive(true);
            }
        }

        [ContextMenu("Close")]
        public void Close()
        {
            if (GameManager.currentPanel == thisPanelName)
            {
                GameManager.currentPanel = CurrentPanel.None;
                panel.SetActive(false);
            }
        }
    }
}

