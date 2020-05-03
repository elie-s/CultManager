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

        private void Update()
        {
            //currentIsland = GameManager.currentIsland;
            //index = (int)currentIsland - 1;
            if (Gesture.Movement.magnitude > 0.5f)
            {

                if (GameManager.currentIsland != CurrentIsland.Origin && GameManager.currentIsland != CurrentIsland.SummonArea)
                {
                    index = (int)GameManager.currentIsland - 1;
                    ChangeIsland();
                }
            }


        }

        void ChangeIsland()
        {
            if (Gesture.DeltaMovement.y > 0.05f)
            {
                if (index > 0)
                {
                    controller.Transition(index--);
                }
                else
                {
                    controller.Transition(2);
                }

            }
            else if (Gesture.DeltaMovement.y < -0.05f)
            {
                if (index < 2)
                {
                    controller.Transition(index++);
                }
                else
                {
                    controller.Transition(0);
                }
            }
        }
    }
}

