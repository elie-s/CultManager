using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class WolrdObjectInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTouch = default;

        private Camera cam;
        private bool interactable = true;

        private void OnEnable()
        {
            cam = Camera.main;
        }

        void Update()
        {
            if (interactable && Input.GetMouseButton(0)) Interact();
        }

        public void SetInteractable(bool _interactable)
        {
            interactable = _interactable;
        }

        private void Interact()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Transform hitObj = hit.transform;
                if (hitObj.gameObject == transform.gameObject)
                {
                    onTouch.Invoke();
                }
            }
        }
    }
}