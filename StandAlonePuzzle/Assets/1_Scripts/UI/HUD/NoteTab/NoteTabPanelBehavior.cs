using System.Collections;
using System.Collections.Generic;
using CultManager.HexagonalGrid;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class NoteTabPanelBehavior : MonoBehaviour
    {
        [SerializeField] private NoteTabData data = default;
        [SerializeField] private PuzzleData puzzle = default;
        [SerializeField] private Transform starPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private LerpCurve lerpCurve;
        [SerializeField] private PuzzleDisplay display;

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform Togglebutton;

        [SerializeField] private CurrentPanel thisPanelName;

        [SerializeField] private float puzzleScale;
        CurrentPanel temp = CurrentPanel.None;

        private string direction = "right";

        [SerializeField] private float lerpValue = 0;

        void Start()
        {
            Togglebutton.localScale = new Vector3(Togglebutton.localScale.x, Togglebutton.localScale.y, Togglebutton.localScale.z);
        }

        public void SetNoteTabSegments()
        {
            data.noteTabSegments.Clear();
            for (int i = 0; i < puzzle.puzzle.Count; i++)
            {
                NoteTabSegment segment = new NoteTabSegment(puzzle.puzzle[i].segment, 0);
                data.AddSegment(segment);
            }
        }

        public void SetIndex(Segment segment, int index)
        {
            for (int i = 0; i < data.noteTabSegments.Count; i++)
            {
                if (data.noteTabSegments[i].segment.Equals(segment))
                {
                    Debug.Log("Reached123");
                    data.noteTabSegments[i].SetColorIndex(index);
                }
            }
        }

        public int GetIndex(Segment segment)
        {
            Debug.Log("Reached321");
            int index = 0;
            for (int i = 0; i < data.noteTabSegments.Count; i++)
            {
                if (data.noteTabSegments[i].segment.Equals(segment))
                {
                    index = data.noteTabSegments[i].colorIndex;
                }
            }
            return index;
        }

        // Update is called once per frame
        void Update()
        {
            //EnableButton();

            switch (direction)
            {
                case "left":
                    {
                        LerpMovement(starPoint, endPoint);
                    }
                    break;

                case "right":
                    {
                        LerpMovement(endPoint, starPoint);
                    }
                    break;

                default:
                    {
                        direction = "right";
                    }
                    break;
            }
        }

        public void EnableButton()
        {
            if (GameManager.currentPanel == CurrentPanel.PuzzlePanel || GameManager.currentPanel == CurrentPanel.DemonBook || GameManager.currentPanel == thisPanelName)
            {
                Togglebutton.gameObject.SetActive(true);
            }
            else
            {
                Togglebutton.gameObject.SetActive(false);
            }
        }

        public void LerpMovement(Transform initial, Transform target)
        {
            if (lerpValue < 1f)
            {
                lerpValue += lerpCurve.lerpSpeed * Time.deltaTime;
                Vector2 currentPos = Vector2.Lerp(transform.position, target.position, lerpValue);
                rectTransform.position = currentPos;
            }
        }

        public void PanelToggleButton()
        {
            switch (direction)
            {
                case "left":
                    {
                        direction = "right";
                        lerpValue = 0;
                        GameManager.currentPanel = temp;
                    }
                    break;

                case "right":
                    {
                        Display();
                        direction = "left";
                        lerpValue = 0;
                        temp = GameManager.currentPanel;
                        GameManager.currentPanel = thisPanelName;
                    }
                    break;

                default:
                    {
                        direction = "right";
                        lerpValue = 0;
                        GameManager.currentPanel = temp;
                    }
                    break;
            }
            Togglebutton.localScale = new Vector3(-Togglebutton.localScale.x, Togglebutton.localScale.y, Togglebutton.localScale.z);

        }

        public void Display()
        {
            display.DisplayPuzzle(puzzleScale);

        }


    }
}

