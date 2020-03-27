using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class ObjectInteraction : MonoBehaviour
    {
        public Material hoverMat;
        public Material selectedMat;
        public ObjectState selectionState;
        public static bool isSelected;
        Material tempMat;
        MeshRenderer meshRend, _meshRend;
        Transform _hitObj;

        void Start()
        {

        }

        void Update()
        {
            if (!isSelected)
            {
                if (selectionState != ObjectState.Selected)
                {
                    if (_hitObj != null)
                    {
                        _meshRend = _hitObj.GetComponent<MeshRenderer>();
                        _meshRend.material = tempMat;
                        selectionState = ObjectState.Hovering;
                        _hitObj = null;
                    }
                    else
                    {
                        selectionState = ObjectState.None;
                    }
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Transform hitObj = hit.transform;
                    if (hitObj.gameObject == transform.gameObject)
                    {
                        if (selectionState != ObjectState.Selected)
                        {
                            meshRend = hitObj.GetComponent<MeshRenderer>();
                            if (meshRend != null)
                            {
                                tempMat = meshRend.material;
                                meshRend.material = hoverMat;
                            }
                            _hitObj = hitObj;
                            if (Input.GetMouseButtonDown(0))
                            {
                                selectionState = ObjectState.Selected;
                                isSelected = true;
                                meshRend.material = selectedMat;
                            }
                        }
                    }
                    /*else
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            selectionState = ObjectState.None;
                        }
                    }*/
                }
            }
            
        }
    }
}

