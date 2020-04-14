using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public struct CameraTarget
    {
        public Vector2 position;
        public float size;

        public CameraTarget(Vector2 _position, float _size)
        {
            position = _position;
            size = _size;
        }

        public CameraTarget(Camera _cam)
        {
            position = _cam.transform.localPosition;
            size = _cam.orthographicSize;
        }
    }
}