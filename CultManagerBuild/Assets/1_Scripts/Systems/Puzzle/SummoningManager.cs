using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class SummoningManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager = default;

        public void Open()
        {
            gameManager.SetCameraState(CameraController.CameraState.Puzzle);
        }

        public void Close()
        {
            gameManager.SetCameraState(CameraController.CameraState.Default);
        }
    }
}