using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Animations/Animation Set")]
public class AnimationSet : ScriptableObject
{
    public AnimationCurve curve;
    public float animationDuration;
}
