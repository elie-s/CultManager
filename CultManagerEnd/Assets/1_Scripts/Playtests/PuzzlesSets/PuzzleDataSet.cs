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
        public List<PuzzleSegment> mortimer;
        public List<PuzzleSegment> gmork;
        public List<PuzzleSegment> moloch;
        public List<PuzzleSegment> debilman;
        public List<PuzzleSegment> kotkar;
        public List<PuzzleSegment> khortho;
        public List<PuzzleSegment> kiram;
        public List<PuzzleSegment> balthazar;
        public List<PuzzleSegment> otto;
        public List<PuzzleSegment> korol;


        public void AddPuzzle(DemonName _demon, List<PuzzleSegment> _puzzle)
        {
            switch (_demon)
            {
                case DemonName.None:
                    break;
                case DemonName.Mortimer:
                    mortimer = _puzzle;
                    break;
                case DemonName.Gmork:
                    gmork = _puzzle;
                    break;
                case DemonName.Moloch:
                    moloch = _puzzle;
                    break;
                case DemonName.Debilman:
                    debilman = _puzzle;
                    break;
                case DemonName.Kotkar:
                    kotkar = _puzzle;
                    break;
                case DemonName.Khortho:
                    khortho = _puzzle;
                    break;
                case DemonName.Kiram:
                    kiram = _puzzle;
                    break;
                case DemonName.Balthazar:
                    balthazar = _puzzle;
                    break;
                case DemonName.Otto:
                    otto = _puzzle;
                    break;
                case DemonName.Korol:
                    korol = _puzzle;
                    break;
                default:
                    break;
            }

            save = false;
        }

        public List<PuzzleSegment> GetPuzzle(DemonName _demon)
        {
            switch (_demon)
            {
                case DemonName.None:
                    break;
                case DemonName.Mortimer:
                    return mortimer;
                case DemonName.Gmork:
                    return gmork;
                case DemonName.Moloch:
                    return moloch;
                case DemonName.Debilman:
                    return debilman;
                case DemonName.Kotkar:
                    return kotkar;
                case DemonName.Khortho:
                    return khortho;
                case DemonName.Kiram:
                    return kiram;
                case DemonName.Balthazar:
                    return balthazar;
                case DemonName.Otto:
                    return otto;
                case DemonName.Korol:
                    return korol;
                default:
                    break;
            }

            return mortimer;
        }
    }
}