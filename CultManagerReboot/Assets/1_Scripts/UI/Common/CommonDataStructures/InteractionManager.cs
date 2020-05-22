using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class InteractionManager : MonoBehaviour
    {
        [SerializeField] public CurrentIsland currentIsland;
        [SerializeField] public CameraController controller;
        [SerializeField] private int index = 0;

        [SerializeField] private bool isRunning;

        private void Update()
        {
            currentIsland = GameManager.currentIsland;
            //index = (int)currentIsland - 1;

            if (GameManager.currentIsland != CurrentIsland.Origin && GameManager.currentIsland != CurrentIsland.SummonArea && !isRunning)
            {
                index = (int)GameManager.currentIsland - 1;
                //ChangeIsland();
            }
        }

        void ChangeIsland()
        {
            if (Gesture.DeltaMovement.y > 0.05f)
            {
                isRunning = true;
                if (index > 0)
                {
                    controller.Transition(index--);
                }
                else
                {
                    controller.Transition(3);
                }
                Invoke("FlipBool", 0.25f);

            }
            else if (Gesture.DeltaMovement.y < -0.05f)
            {
                isRunning = true;
                if (index < 3)
                {
                    controller.Transition(index++);
                }
                else
                {
                    controller.Transition(0);
                }
                Invoke("FlipBool", 0.25f);
            }
        }

        void FlipBool()
        {
            if (isRunning)
            {
                isRunning = false;
            }
        }
    }
}

