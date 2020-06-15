using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CultManager.HexagonalGrid;

namespace CultManager
{
    public class PuzzeManager : MonoBehaviour
    {
        [SerializeField] private PuzzleData data = default;
        [SerializeField] private StatuesData statuesData = default;
        [SerializeField] private DemonData demonData = default;
        [SerializeField] private PuzzleSaveData puzzleSaveData = default;
        [SerializeField] private DemonManager demonManager = default;
        [SerializeField] private BloodBankManager bloodManager = default;
        [SerializeField] private MoneyManager resourcesManager = default;
        [SerializeField] private PuzzleDisplay display = default;
        [SerializeField] private float scale = 1.0f;
        [SerializeField] private PatternGenerationSettings[] settings = default;
        [SerializeField] private PatternGenerationSettings patternSettings = default;
        [SerializeField] private PuzzleSettingsSet generationSettings = default;

        [SerializeField] private bool altarComplete = default;
        [SerializeField] private SpriteRenderer background = default;

        private Pattern gridConstruction;
        [SerializeField] private List<Segment> patternSegments = default;
        [SerializeField] private UnityEvent onPerfectSpawnSummoned = default;

        private Spawn lastSpawn;
        public static bool resurrection;

        public void LoadData()
        {
            display?.DisplayPuzzle(scale);
            ClearSelection();
            UpdateInWorldPuzzle();
        }

        public void ResetData()
        {
            if (!PuzzleSaveManager.puzzleSaveLoaded) puzzleSaveData.ResetData();
            Generate();
            display?.DisplayPuzzle(scale);
            
            //Generate();
        }

        public void SAResetData()
        {
            if (!PuzzleSaveManager.puzzleSaveLoaded) puzzleSaveData.ResetData();
            SAResetCult();
        }

        public void GetDemonPuzzle()
        {
            data.puzzle = statuesData.currentPuzzle;
            UpdateInWorldPuzzle();

            if ((int)statuesData.currentDemon < 4) data.layers = 2;
            else data.layers = 3;

            display?.DisplayPuzzle(scale);
        }

        private void UpdateInWorldPuzzle()
        {
            InWorldPuzzleDisplayer[] displayers = FindObjectsOfType<InWorldPuzzleDisplayer>();

            foreach (InWorldPuzzleDisplayer disp in displayers)
            {
                disp.UpdateDisplay();
            }
        }

        public void ResetCult(int level)
        {
            Generate();
            display?.DisplayPuzzle(scale);
            if (level > 5) Generate();
        }

        public void SAResetCult()
        {
            //SAGenerate();
            statuesData.SetDemon((DemonName)puzzleSaveData.currentIndex);
            data.puzzle = statuesData.currentPuzzle;
            puzzleSaveData.Increment();
            display?.DisplayPuzzle(scale);
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

        public void SAGenerate()
        {
            PatternGenerationSettings[] puzzleSettings = generationSettings.set[puzzleSaveData.currentIndex].puzzleSettings;
            PatternGenerationSettings demonPattern = generationSettings.set[puzzleSaveData.currentIndex].demonPattern;

            HexGrid grid = new HexGrid(scale, 1);
            Pattern pattern = new Pattern(grid, puzzleSettings[0]);
            data.puzzle = new List<PuzzleSegment>();
            data.layers = puzzleSettings.Length;

            for (int i = 1; i < puzzleSettings.Length; i++)
            {
                grid.DoubleGrid();
                pattern.DoubleSize();
                pattern.AddToShape(puzzleSettings[i], true);
            }

            for (int i = 0; i < pattern.stepSegments.Count; i++)
            {
                for (int j = 0; j < pattern.stepSegments[i].Count; j++)
                {
                    data.puzzle.Add(new PuzzleSegment(pattern.stepSegments[i][j], (BloodType)(i)));
                    Debug.Log((BloodType)(i));
                }
            }
            puzzleSaveData.AddGeneration(puzzleSettings, data.puzzle.ToArray());
            grid = new HexGrid(pattern);
            pattern = new Pattern(grid, demonPattern);


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

            Debug.Log("SA Puzzle generated");

        }

        [ContextMenu("Generate")]
        public void Generate()
        {
            HexGrid grid = new HexGrid(scale, 1);
            Pattern pattern = new Pattern(grid, settings[0]);
            data.puzzle = new List<PuzzleSegment>();
            data.layers = settings.Length;

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
            puzzleSaveData?.AddGeneration(settings, data.puzzle.ToArray());
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
            ShowSymbol();
            AddPattern();
            ClearSelection();
            bloodManager.ResetTempBanks();
        }

        public void ShowSymbol()
        {
            GatherCurrentPatternSegments();
            display.ShowSymbol(patternSegments.ToArray());
        }

        public void EraseSymbol()
        {
            display.EraseSymbol();
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

        public void RegisterNewExperiment()
        {
            GatherCurrentPatternSegments();
            lastSpawn = demonManager.AddNewExperiment(3, patternSegments.ToArray(), data.ComputePatternMatchCount(patternSegments.ToArray()), data.GatherPatternSegments().Length);
            Demon demon= demonData.ReturnDemonForSpawn(lastSpawn);
            //puzzleSaveData.generations[puzzleSaveData.currentIndex - 1].AddAttempt(demon);
            if (lastSpawn.patternAccuracy == 1.0f) onPerfectSpawnSummoned.Invoke();

            if (demon.patternSegments != 0) resourcesManager?.Increase(0, Mathf.CeilToInt(demon.patternSegments * demon.accuracy));
        }

        public void SummonExperiment(float _delay)
        {
            if(resurrection) demonManager.SummonDemon(lastSpawn).Summon(_delay);
            else demonManager.SummonSpawn(lastSpawn).Summon(_delay);
        }

        public void Debugging(Demon instance)
        {
            Debug.Log("Demon Time" + instance.deathTime);
            Debug.Log("Demon Loot" + instance.lootBonus);
            Debug.Log("Demon Segments" + instance.segments.Length);
        }


        private void OnDrawGizmosSelected()
        {
            //if (data.puzzle != null)
            //{
            //    foreach (PuzzleSegment segment in data.puzzle)
            //    {
            //        Gizmos.color = segment.patternSegment ? Color.yellow : Color.blue;
            //        Gizmos.DrawLine(Node.WorldPosition(segment.a, scale) + (Vector2)transform.position, Node.WorldPosition(segment.b, scale) + (Vector2)transform.position);
            //    }
            //}
        }
    }
}