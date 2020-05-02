﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class PuzzeManager : MonoBehaviour
    {
        [SerializeField] private PuzzleData data = default;
        [SerializeField] private BloodBankManager bloodManager = default;
        [SerializeField] private DemonManager demonManager = default;
        [SerializeField] private AltarManager altarManager = default;
        [SerializeField] private PuzzleDisplay display = default;
        [SerializeField] private float scale = 1.0f;
        [SerializeField] private PatternGenerationSettings[] settings;
        [SerializeField] private PatternGenerationSettings patternSettings;

        [SerializeField] private bool altarComplete;
        [SerializeField] private SpriteRenderer background;

        private Pattern gridConstruction;
        [SerializeField]private List<Segment> patternSegments;


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
        }

        public void ResetCult(int level)
        {
            Generate();
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

        public void CompletedAltar()
        {
            altarComplete = true;
            background.color = new Color(1, 0, 0, 1);
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
                    data.puzzle.Add(new PuzzleSegment(pattern.stepSegments[i][j], (BloodType)(i + 1)));
                }
            }

            grid = new HexGrid(pattern);
            pattern = new Pattern(grid, patternSettings);

            foreach (PuzzleSegment segment in data.puzzle)
            {
                for (int i = 0; i < pattern.segments.Count; i++)
                {
                    if(segment.IsSegment(pattern.segments[i]))
                    {
                        segment.SetAsPatternSegment();
                        break;
                    }
                }
            }

            display?.DisplayPuzzle(scale);
        }


        public bool ValidatePattern()
        {
            int ctr = 0;
            for (int i = 0; i < data.puzzle.Count; i++)
            {
                if (data.puzzle[i].selected)
                {
                    patternSegments.Add(data.puzzle[i].segment);
                    Debug.Log(data.puzzle[i]);
                    ctr++;
                }

            }
            return (ctr > 2);  
        }

        public void SummonIt()
        {

            if (ValidatePattern())
            {
                AddPattern();
                ClearSelection();
            }
            else
            {
                Debug.Log("SorryBoi");
                ClearSelection();
                FailedPattern();
            }
        }

        public void AddPattern()
        {
            if (altarComplete)
            {
                demonManager.CreateNewDemon(1, patternSegments.ToArray());
            }
            else
            {
                demonManager.CreateNewPersistentDemon(1);
            }
            
            ClearSelection();
            patternSegments.Clear();
        }


        public void Debugging(Demon instance)
        {
            Debug.Log("Demon Time" + instance.spawnTime);
            Debug.Log("Demon Loot" + instance.lootBonus);
            Debug.Log("Demon Segments" + instance.segments.Length);
        }


        private void OnDrawGizmosSelected()
        {
            if(data.puzzle != null)
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