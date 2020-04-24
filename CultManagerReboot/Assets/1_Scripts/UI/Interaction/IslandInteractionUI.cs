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
            returnButton.SetActive(!CameraController.isAtOrigin);
        }
    }
}

