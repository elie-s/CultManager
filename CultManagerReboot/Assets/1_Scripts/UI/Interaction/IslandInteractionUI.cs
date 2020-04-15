using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class IslandInteractionUI : MonoBehaviour
    {
        [SerializeField] private GameObject[] islandButtons;
        [SerializeField] private GameObject returnButton;

        private void Update()
        {
            if (CameraController.isAtOrigin)
            {
                returnButton.SetActive(false);
                for (int i = 0; i < islandButtons.Length; i++)
                {
                    islandButtons[i].SetActive(true);
                }
            }
            else
            {
                returnButton.SetActive(true);
                for (int i = 0; i < islandButtons.Length; i++)
                {
                    islandButtons[i].SetActive(false);
                }
            }
        }
    }
}

