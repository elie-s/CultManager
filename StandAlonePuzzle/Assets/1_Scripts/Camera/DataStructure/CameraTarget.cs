using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public struct CameraTarget
    {
        public Transform waypoint;
        public float size;

        public CameraTarget(Transform _position, float _size)
        {
            waypoint = _position;
            size = _size;
        }

        public CameraTarget(Camera _cam)
        {
            waypoint = _cam.transform;
            size = _cam.orthographicSize;
        }
    }
}