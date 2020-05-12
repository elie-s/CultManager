 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Puzzle/SaveDataSet")]
    public class PuzzleDataSet : ScriptableObject
    {
        public bool save;
        public List<PuzzleSegment> tutorial = new List<PuzzleSegment>();
        public List<List<PuzzleSegment>> levelOne = new List<List<PuzzleSegment>>();
        public int levelOneIndex;
        public List<List<PuzzleSegment>> levelTwo = new List<List<PuzzleSegment>>();
        public int levelTwoIndex;
        public List<List<PuzzleSegment>> levelThree = new List<List<PuzzleSegment>>();
        public int levelThreeIndex;
        public List<List<PuzzleSegment>> levelFor = new List<List<PuzzleSegment>>();
        public int levelForIndex;
        public List<List<PuzzleSegment>> levelFive = new List<List<PuzzleSegment>>();
        public int levelFiveIndex;

        public void AddPuzzle(Level _level, List<PuzzleSegment> _puzzle)
        {
            switch (_level)
            {
                case Level.Tutorial:
                    tutorial = _puzzle;
                    break;
                case Level.One:
                    levelOne.Add(_puzzle);
                    break;
                case Level.Two:
                    levelTwo.Add(_puzzle);
                    break;
                case Level.Three:
                    levelThree.Add(_puzzle);
                    break;
                case Level.For:
                    levelFor.Add(_puzzle);
                    break;
                case Level.Five:
                    levelFive.Add(_puzzle);
                    break;
                default:
                    break;
            }

            save = false;
        }

        public List<PuzzleSegment> GetPuzzle(int _level)
        {
            switch (_level)
            {
                case 0:
                    return tutorial;
                case 1:
                    return levelOne[levelOneIndex];
                case 2:
                    return levelTwo[levelTwoIndex];
                case 3:
                    return levelThree[levelThreeIndex];
                case 4:
                    return levelFor[levelForIndex];
                case 5:
                    return levelFive[levelFiveIndex];

                default:
                    return null;
            }
        }

        public enum Level
        {
            Tutorial, 
            One,
            Two, 
            Three,
            For,
            Five
        }
    }
}