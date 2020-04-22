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


        void Update()
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

