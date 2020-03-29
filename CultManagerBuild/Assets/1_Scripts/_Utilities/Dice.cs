using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    public readonly int sides;

    public Dice(int _sides = 6)
    {
        if (_sides < 4) throw new System.NotImplementedException("Dice must have more than 3 faces.");

        sides = _sides;
    }

    public int Roll(int _throws = 1)
    {
        int result = 0;

        for (int i = 0; i < _throws; i++)
        {
            result += Random.Range(0, 6) + 1;
        }

        return result;
    }
}
