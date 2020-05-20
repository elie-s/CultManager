using UnityEngine;

[CreateAssetMenu(menuName ="Tools/ScriptableRandom")]
public class ScriptableRandom : ScriptableObject
{
    [SerializeField] private AnimationCurve curve = default;
    
    public float Random => curve.Evaluate(UnityEngine.Random.value);
    public bool Binary => Random > 0.5f;


    public float GetValue(float _value)
    {
        return curve.Evaluate(_value);
    }
}
