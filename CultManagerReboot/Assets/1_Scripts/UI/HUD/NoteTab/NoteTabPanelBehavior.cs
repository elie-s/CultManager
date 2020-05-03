using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class NoteTabPanelBehavior : MonoBehaviour
    {
        [SerializeField] private Transform starPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private LerpCurve lerpCurve;
        [SerializeField] private PuzzleDisplay display;

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform Togglebutton;

        [SerializeField] private CurrentPanel thisPanelName;

        private bool startMove;
        private string direction="right";
        void Start()
        {
            Togglebutton.localScale = new Vector3(Togglebutton.localScale.x, Togglebutton.localScale.y, Togglebutton.localScale.z);
            
        }

        // Update is called once per frame
        void Update()
        {
            if (startMove)
            {
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
            
        }


        public void LerpMovement(Transform initial, Transform target)
        {
            if (Vector2.Distance(transform.position, target.position) > 1f)
            {
                rectTransform.position = Vector2.Lerp(transform.position, target.position,Time.deltaTime);
                //lerpCurve.lerpCurve.Evaluate(lerpCurve.lerpValue) * Time.deltaTime
            }
            else
            {
                startMove = false;
            }
        }

        public void PanelToggleButton()
        {
            if (!startMove)
            {
                if (GameManager.currentPanel == CurrentPanel.None || GameManager.currentPanel == thisPanelName)
                {
                    switch (direction)
                    {
                        case "left":
                            {
                                direction = "right";
                                GameManager.currentPanel = CurrentPanel.None;
                            }
                            break;

                        case "right":
                            {
                                Display();
                                direction = "left";
                                GameManager.currentPanel = thisPanelName;
                            }
                            break;

                        default:
                            {
                                direction = "right";
                                GameManager.currentPanel = CurrentPanel.None;
                            }
                            break;
                    }
                    Togglebutton.localScale = new Vector3(-Togglebutton.localScale.x, Togglebutton.localScale.y, Togglebutton.localScale.z);
                    startMove = true;
                }
            } 
        }

        public void Display()
        {
            display.DisplayPuzzle(50f);
        }
    }
}

