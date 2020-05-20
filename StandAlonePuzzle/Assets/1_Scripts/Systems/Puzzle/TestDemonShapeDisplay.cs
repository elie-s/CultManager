using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class TestDemonShapeDisplay : MonoBehaviour
    {
        [SerializeField] private PuzzleDisplay display = default;
        [SerializeField] private DemonData data = default;
        [SerializeField] private int index = 0;

        [ContextMenu("Show")]
        public void Show()
        {
            display.HighlightShape(data.demons[index].segments);
        }

    }
}