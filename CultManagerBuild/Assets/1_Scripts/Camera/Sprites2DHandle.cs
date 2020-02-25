using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [ExecuteAlways]
    public class Sprites2DHandle : MonoBehaviour
    {
        [SerializeField] private Transform cam = default;
        [SerializeField] private Mode mode = default;

        private void OnEnable()
        {
            SetCamera();
        }

        // Update is called once per frame
        void Update()
        {
            HandleSprite();
        }

        private void HandleSprite()
        {
            switch (mode)
            {
                case Mode.Front:
                    Front();
                    break;
                case Mode.Orientation:
                    Orientation();
                    break;
                default:
                    break;
            }
        }

        private void Front()
        {
            transform.forward = (cam.position - transform.position) * -1;
        }

        private void Orientation()
        {
            transform.forward = cam.forward * -1;
        }

        private void SetCamera()
        {
            if (cam) return;

            cam = FindObjectOfType<CameraController>()?.GetCamTransform();
            if (!cam) cam = Camera.main.transform;
        }

        public enum Mode { Front, Orientation}
    }
}