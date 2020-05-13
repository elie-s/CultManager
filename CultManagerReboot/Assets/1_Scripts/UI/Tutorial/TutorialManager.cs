using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace CultManager
{
    public class TutorialManager : MonoBehaviour
    {
        public int tutorialStep = 0;
        [SerializeField] private GameObject mask;
        [SerializeField] private RectTransform textObject;
        [SerializeField] private TMP_Text text;
        [SerializeField] private MaskLocation[] maskLocations;

        private void Start()
        {
            NextStep(0);
        }

        public void NextStep(int i)
        {
            if (i == tutorialStep)
            {
                if (i == 16)
                {
                    tutorialStep++;
                    mask.SetActive(false);
                    textObject.gameObject.SetActive(false);
                }
                else
                {
                    tutorialStep++;
                    mask.transform.position = maskLocations[i].maskPosition.position;
                    mask.transform.localScale = new Vector3(maskLocations[i].scaleFactor, maskLocations[i].scaleFactor, 1);
                    mask.SetActive(maskLocations[i].maskActive);

                    text.text = maskLocations[i].displayText;
                    textObject.position = maskLocations[i].textPosition.position;
                }
                
            }
        }
    }
}

