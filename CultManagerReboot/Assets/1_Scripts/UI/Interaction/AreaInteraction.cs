using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0414
namespace CultManager
{
    public class AreaInteraction : MonoBehaviour
    {
        [SerializeField] private Collider2D col = default;
        [SerializeField] private UnityEvent onAreaClicked = default;

        public static bool isUsed;

        [SerializeField] private bool current = default;
        [SerializeField] private bool local = default;

        public CurrentPanel reqdPanel = CurrentPanel.None;
        public CurrentIsland reqdIsland = CurrentIsland.All;

        void Update()
        {
            current = isUsed;
            if (reqdIsland == CurrentIsland.All || GameManager.currentIsland == reqdIsland)
            {
                if (GameManager.currentPanel == CurrentPanel.None || GameManager.currentPanel == reqdPanel)
                {
                    col.enabled = true;
                    if ( /*Input.GetMouseButtonDown(0) || */ Gesture.QuickTouch)
                    {
                        Vector3 worldPos = CameraController.CurrentCam.ScreenToWorldPoint(Input.mousePosition);
                        if (col.OverlapPoint(worldPos) && !isUsed)
                        {
                            local = true;
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
            local = false;
        }
    }
}

