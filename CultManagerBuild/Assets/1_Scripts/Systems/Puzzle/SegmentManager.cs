using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CultManager
{
    public class SegmentManager : MonoBehaviour
    {
        SegmentBehavior[] segments;
        [SerializeField] private bool success;
        [SerializeField] private UnityEvent castSuccessful = default;
        [SerializeField] private UnityEvent castUnSuccessful = default;
        void Start()
        {
            segments = new SegmentBehavior[transform.childCount];
            GatherChildren(transform, segments);
        }

        void GatherChildren(Transform parent, SegmentBehavior[] children)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                children[i] = parent.GetChild(i).gameObject.GetComponent<SegmentBehavior>();
            }
        }

        void Update()
        {

        }

        public void Summon()
        {
            int ctr = 0;
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i].segmentActive)
                {
                    ctr++;
                }
            }
            if (ctr >= 3)
            {
                success = true;
                castSuccessful.Invoke();
            }
            else
            {
                success = false;
                castUnSuccessful.Invoke();
            }
        }
    }
}

