﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace CultManager
{
    public class AreaInteraction : MonoBehaviour
    {
        [SerializeField] private Collider2D col = default;
        [SerializeField] private UnityEvent onAreaClicked = default;

        public static bool isUsed;

        public CurrentPanel reqdPanel = CurrentPanel.None;
        public CurrentIsland reqdIsland = CurrentIsland.All;

        private Camera cam => CameraController.CurrentCam;

        void Update()
        {
            if (reqdIsland == CurrentIsland.All || GameManager.currentIsland == reqdIsland)
            {
                if (GameManager.currentPanel == CurrentPanel.None || GameManager.currentPanel == reqdPanel)
                {
                    col.enabled = true;
                    if (Gesture.QuickTouch)
                    {
                        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
                        if (col.OverlapPoint(worldPos) && !isUsed)
                        {
                            isUsed = true;
                            onAreaClicked.Invoke();
                            Invoke("FlipBool", 0.25f);
                        }
                    }
                }
                else
                {
                    DisableCollider();
                }

            }
            else
            {
                DisableCollider();
            }

        }

        void DisableCollider()
        {
            col.enabled = false;
        }

        void FlipBool()
        {
            if (isUsed)
            {
                isUsed = false;
            }
        }
    }
}
