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
        public List<PuzzleSegment> levelOne = new List<PuzzleSegment>();
        public List<PuzzleSegment> levelTwo = new List<PuzzleSegment>();
        public List<PuzzleSegment> levelThree = new List<PuzzleSegment>();
        public List<PuzzleSegment> levelFor = new List<PuzzleSegment>();
        public List<PuzzleSegment> levelFive = new List<PuzzleSegment>();

        public void AddPuzzle(Level _level, List<PuzzleSegment> _puzzle)
        {
            switch (_level)
            {
                case Level.Tutorial:
                    tutorial = _puzzle;
                    break;
                case Level.One:
                    levelOne = _puzzle;
                    Debug.Log("New LevelOne puzzle Added.");
                    break;
                case Level.Two:
                    levelTwo = _puzzle;
                    Debug.Log("New levelTwo puzzle Added.");
                    break;
                case Level.Three:
                    levelThree = _puzzle;
                    Debug.Log("New levelThree puzzle Added.");
                    break;
                case Level.For:
                    levelFor = _puzzle;
                    Debug.Log("New levelFor puzzle Added. ");
                    break;
                case Level.Five:
                    levelFive = _puzzle;
                    Debug.Log("New levelFive puzzle Added.");
                    break;
                default:
                    break;
            }

            save = false;
        }

        public List<PuzzleSegment> GetPuzzle(int _level)
        {
            Debug.Log("Puzzle gotten");

            switch (_level)
            {
                case 0:
                    return tutorial;
                case 1:
                    return levelOne;
                case 2:
                    return levelTwo;
                case 3:
                    return levelThree;
                case 4:
                    return levelFor;
                case 5:
                    return levelFive;

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