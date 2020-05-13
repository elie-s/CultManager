using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public struct QuickFeedback
    {
        public int value;
        public GameDataSet data;
        public int island;
        public int panel;

        public QuickFeedback(QuickFeedbackValue _value, GameDataSet _data)
        {
            value = (int)_value;
            data = _data;

            island = (int)GameManager.currentIsland;
            panel = (int)GameManager.currentPanel;
        }

        public override string ToString()
        {
            return value + DataRecorder.Separation +
                    island + DataRecorder.Separation +
                    panel + DataRecorder.Separation +
                    data;
        }
    }

    public enum QuickFeedbackValue
    {
        Negative = -1,
        Bug,
        Positive
    }
}