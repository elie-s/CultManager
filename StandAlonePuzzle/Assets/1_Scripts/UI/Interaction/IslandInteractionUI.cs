using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class IslandInteractionUI : MonoBehaviour
    {
        [SerializeField] private GameObject returnButton;

        private void Update()
        {
            if (GameManager.currentPanel == CurrentPanel.None && GameManager.currentIsland != CurrentIsland.Origin)
            {
                returnButton.SetActive(true);
            }
            else
            {
                returnButton.SetActive(false);
            }
        }
    }
}

