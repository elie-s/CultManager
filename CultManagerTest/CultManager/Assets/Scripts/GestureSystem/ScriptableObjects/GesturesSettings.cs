﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Gestures/Manager Settings")]
    public class GesturesSettings : ScriptableObject
    {
        public float quickTouchDelay = 0.15f;
        public float doubleTapDelay = 0.20f;
    }
}