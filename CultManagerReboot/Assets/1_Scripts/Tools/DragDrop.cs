using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class DragDrop : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        private float startPosX;
        private float startPosY;
        public bool isBeingHeld = false;
        public bool isinZone = false;

        public bool canDrag;

        private Vector2 initialPos;


        private void OnEnable()
        {
            if (!cam) cam = Camera.main;
            initialPos = transform.position;
        }

        private void Update()
        {
            if (isBeingHeld)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = cam.ScreenToWorldPoint(mousePos);
                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
            }
            else
            {
                if (!isinZone)
                {
                    transform.position = initialPos;
                }
            }
        }

        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                startPosX = mousePos.x - this.transform.localPosition.x;
                startPosY = mousePos.y - this.transform.localPosition.y;

                isBeingHeld = true;
            }
        }

        private void OnMouseUp()
        {
            isBeingHeld = false;
        }

    }
}

