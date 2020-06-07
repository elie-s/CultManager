using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class PanningArea
    {
        public Vector2 position { get; private set; }


        public float zoom { get; private set; }
        public float zoomValue => Mathf.Lerp(0.0f, maxZoom - baseCamSize, zoom);
        public float camSize => baseCamSize + zoomValue;

        public float height => -zoomValue * 2.0f;
        public float width => height / (Screen.height / Screen.width) + minSize;
        public Vector2 span => new Vector2(width, height) / 2.0f;
        public Vector2 bottomLeft => position - span;
        public Vector2 topRight => position + span;
        public float panningSpeed => baseSpeed * (camSize);

        private float baseCamSize;
        private float maxZoom;
        private float minSize;
        private float baseSpeed;

        public PanningArea(CameraTarget _camtarget, float _maxZoom, float _minSize, float _baseSpeed)
        {
            position = _camtarget.waypoint.position;
            baseCamSize = _camtarget.size;

            zoom = 0.0f;
            maxZoom = _maxZoom;
            minSize = _minSize;
            baseSpeed = _baseSpeed;
        }

        public void ZoomIn(float _value)
        {
            zoom = Mathf.Clamp(zoom + _value, 0.0f, 1.0f);
        }

        public void ZoomOut(float _value)
        {
            zoom = Mathf.Clamp(zoom - _value, 0.0f, 1.0f);
        }

        public void SetZoom(float _value)
        {
            zoom = Mathf.Clamp(_value, 0.0f, 1.0f);
        }

        public bool Contains(Vector2 _position)
        {
            return _position.x >= bottomLeft.x
                && _position.x <= topRight.x
                && _position.y >= bottomLeft.y
                && _position.y <= topRight.y;
        }

        public Vector2 GetAreaPosition(Vector2 _worldPos)
        {
            if (zoom == 0.0f) return Vector2.zero;

            Vector2 relativePos = _worldPos - position;

            return new Vector2(relativePos.x / span.x, relativePos.y / span.y);
        }

        public Vector2 WorldFromAreaPosition(Vector2 _areaPos)
        {
            return new Vector2(_areaPos.x * span.x, _areaPos.y * span.y) + position;
        }
    }
}