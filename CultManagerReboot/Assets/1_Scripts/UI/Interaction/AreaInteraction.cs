using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace CultManager
{
    public class AreaInteraction : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private Collider2D col;
        [SerializeField] private UnityEvent onAreaClicked = default;

        public CurrentPanel reqdPanel=CurrentPanel.None; 
        public CurrentIsland reqdIsland=CurrentIsland.All; 

        private void OnEnable()
        {
            if (!cam) cam = Camera.main;
        }

        void Update()
        {
            if (GameManager.currentIsland == CurrentIsland.All || GameManager.currentIsland==reqdIsland)
            {
                if (GameManager.currentPanel == CurrentPanel.None || GameManager.currentPanel == reqdPanel)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector3 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
                        if (col.OverlapPoint(worldPos))
                        {
                            onAreaClicked.Invoke();
                        }
                    }
                }
                
            }
            
        }
    }
}

