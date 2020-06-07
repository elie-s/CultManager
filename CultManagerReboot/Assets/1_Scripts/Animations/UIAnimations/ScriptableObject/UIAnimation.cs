using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public abstract class UIAnimation : ScriptableObject
    {
        public abstract IEnumerator Play(float _duration, RectTransform _transform);
    }
}