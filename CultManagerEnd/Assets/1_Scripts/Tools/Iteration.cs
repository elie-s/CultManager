using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iteration
{
    public float timer { get; private set; }
    public float limit { get; private set; }
    public float min { get; private set; }
    public float fraction => timer / (limit - min);
    public float curveEvaluation => curve.Evaluate(fraction);
    public bool isBelowOne => timer < (limit - min);

    private AnimationCurve curve;

    public Iteration(float _limit)
    {
        timer = 0.0f;
        min = 0.0f;
        limit = _limit;
    }

    public Iteration(float _min, float _limit)
    {
        timer = 0.0f;
        min = _min;
        limit = _limit;
    }

    public Iteration(float _limit, AnimationCurve _curve)
    {
        timer = 0.0f;
        min = 0.0f;
        limit = _limit;
        curve = _curve;
    }

    public void Increment(float _value)
    {
        timer += _value;
    }

    public void Increment()
    {
        timer += Time.deltaTime;
    }

    public void FixedIncrement()
    {
        timer += Time.fixedDeltaTime;
    }

    public IEnumerator YieldIncrement()
    {
        yield return null;
        timer += Time.deltaTime;
    }

    public IEnumerator YieldIncrement(float _increment)
    {
        yield return null;
        timer += _increment;
    }

    public IEnumerator YieldFixedIncrement()
    {
        yield return new WaitForFixedUpdate();
        timer += Time.fixedDeltaTime;
    }

    public override string ToString()
    {
        return "Evaluation: " + timer + " / " + (limit - min) + " --> " + fraction;
    }
}
