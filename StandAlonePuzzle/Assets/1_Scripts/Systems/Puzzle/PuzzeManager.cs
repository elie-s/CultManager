using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class PuzzeManager : MonoBehaviour
    {
        [SerializeField] private PuzzleData data = default;
        [SerializeField] private DemonManager demonManager = default;
        [SerializeField] private BloodBankManager bloodManager = default;
        [SerializeField] private PuzzleDisplay display = default;
        [SerializeField] private float scale = 1.0f;
        [SerializeField] private PatternGenerationSettings[] settings;
        [SerializeField] private PatternGenerationSettings patternSettings;

        [SerializeField] private bool altarComplete;
        [SerializeField] private SpriteRenderer background;

        private Pattern gridConstruction;
        [SerializeField] private List<Segment> patternSegments;


        private void Start()
        {
            background.color = new Color(1, 1, 1, 1);
        }

        public void LoadData()
        {
            display?.DisplayPuzzle(scale);
            ClearSelection();
        }

        public void ResetData()
        {
            Generate();
            display?.DisplayPuzzle(scale);
            //Generate();
        }

        public void ResetCult(int level)
        {
            Generate();
            display?.DisplayPuzzle(scale);
            if (level > 5) Generate();
        }

        public void ClearSelection()
        {
            data.ClearSelections();
            display.UnselectAll();
        }

        public void FailedPattern()
        {
            bloodManager.FailedPattern();
        }

        public void ResetBanks()
        {
            bloodManager.ResetTempBanks();
        }


        public void CompletedAltar()
        {
            if (!altarComplete)
            {
                altarComplete = true;
                background.color = new Color(1, 0, 0, 1);
            }

        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            HexGrid grid = new HexGrid(scale, 1);
            Pattern pattern = new Pattern(grid, settings[0]);
            data.puzzle = new List<PuzzleSegment>();

            for (int i = 1; i < settings.Length; i++)
            {
                grid.DoubleGrid();
                pattern.DoubleSize();
                pattern.AddToShape(settings[i], true);
            }

            for (int i = 0; i < pattern.stepSegments.Count; i++)
            {
                for (int j = 0; j < pattern.stepSegments[i].Count; j++)
                {
                    data.puzzle.Add(new PuzzleSegment(pattern.stepSegments[i][j], (BloodType)(i)));
                    Debug.Log((BloodType)(i));
                }
            }

            grid = new HexGrid(pattern);
            pattern = new Pattern(grid, patternSettings);

            foreach (PuzzleSegment segment in data.puzzle)
            {
                for (int i = 0; i < pattern.segments.Count; i++)
                {
                    if (segment.IsSegment(pattern.segments[i]))
                    {
                        segment.SetAsPatternSegment();
                        break;
                    }
                }
            }

            display?.DisplayPuzzle(scale);

            Debug.Log("Puzzle generated");
        }


        public bool ValidatePattern()
        {
            int ctr = 0;
            for (int i = 0; i < data.puzzle.Count; i++)
            {
                if (data.puzzle[i].selected)
                {
                    ctr++;
                }

            }
            return (ctr > 2);
        }

        public void GatherCurrentPatternSegments()
        {
            patternSegments.Clear();
            for (int i = 0; i < data.puzzle.Count; i++)
            {
                if (data.puzzle[i].selected)
                {
                    patternSegments.Add(data.puzzle[i].segment);
                }

            }
        }

        public void SummonIt()
        {
            GatherCurrentPatternSegments();
            AddPattern();
            ClearSelection();
            bloodManager.ResetTempBanks();
        }

        public void AddPattern()
        {
            if (data.ComputePatternMatchCount(patternSegments.ToArray())==data.GatherPatternSegments().Length)
            {
                Debug.Log("Demon");
                demonManager.CreateNewPersistentDemon(1);
            }
            else
            {
                demonManager.CreateNewDemon(3, patternSegments.ToArray(), data.ComputePatternMatchCount(patternSegments.ToArray()),data.GatherPatternSegments().Length);
            }

            ClearSelection();
            patternSegments.Clear();
        }


        public void Debugging(Demon instance)
        {
            Debug.Log("Demon Time" + instance.deathTime);
            Debug.Log("Demon Loot" + instance.lootBonus);
            Debug.Log("Demon Segments" + instance.segments.Length);
        }


        private void OnDrawGizmosSelected()
        {
            if (data.puzzle != null)
            {
                foreach (PuzzleSegment segment in data.puzzle)
                {
                    Gizmos.color = segment.patternSegment ? Color.yellow : Color.blue;
                    Gizmos.DrawLine(Node.WorldPosition(segment.a, scale) + (Vector2)transform.position, Node.WorldPosition(segment.b, scale) + (Vector2)transform.position);
                }
            }
        }
    }
}