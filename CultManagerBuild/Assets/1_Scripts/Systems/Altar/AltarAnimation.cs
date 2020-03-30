using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class AltarAnimation : MonoBehaviour
    {
        [SerializeField] private AltarPartBehavior partBehaviour = default;
        [SerializeField] private CapsuleCollider col = default;
        [SerializeField] private float maxY = 0.0f;
        [SerializeField] private float maxCollider = 0.0f;

        private float startY;
        private float startCol;

        // Start is called before the first frame update
        void Start()
        {
            startY = transform.localPosition.y;
            startCol = col.height;
        }

        // Update is called once per frame
        void Update()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(startY, maxY, partBehaviour.currentBuildPoints.ratio), transform.localPosition.z);
            col.height = Mathf.Lerp(startCol, maxCollider, partBehaviour.currentBuildPoints.ratio);
        }
    }
}