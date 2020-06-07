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
        public CurrentIsland island;

        public CameraTarget(Transform _position, float _size, CurrentIsland _island)
        {
            waypoint = _position;
            size = _size;
            island = _island;
        }

        public CameraTarget(Camera _cam)
        {
            waypoint = _cam.transform;
            size = _cam.orthographicSize;
            island = CurrentIsland.Origin;
        }
    }
}