using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CultManager
{
    public class AltarPartUI : MonoBehaviour
    {
        public AltarPartBehavior altarPartBehavior;
        [Header("Display Data")]
        public TextMeshProUGUI pillarName;
        public TextMeshProUGUI pillarTrivia;
        public TextMeshProUGUI pillarRequirement;
        public GameObject buyButton;
        public GameObject progressImage;


        [Header("Computing Data")]
        public Image progressBar;
        public Text progressRate;


        void Start()
        {
        }

        public void DisplayData()
        {
            pillarName.text = altarPartBehavior.currentAltarPartData.pillarName;
            pillarTrivia.text = altarPartBehavior.currentAltarPartData.pillarTrivia;
            pillarRequirement.text = altarPartBehavior.currentAltarPartData.pillarRequirement;
            buyButton.SetActive(!altarPartBehavior.isBought);
            progressImage.SetActive(altarPartBehavior.isBought);
            progressBar.fillAmount = altarPartBehavior.currentBuildPoints.ratio;
            progressRate.text = Mathf.RoundToInt(altarPartBehavior.currentBuildPoints.percentage).ToString()+"%";
        }



        public void ClosePanel()
        {
            altarPartBehavior.gameObject.GetComponent<ObjectInteraction>().selectionState = ObjectState.None;
            ObjectInteraction.isSelected = false;
        }

        public void Buy()
        {
            altarPartBehavior.BuyBuilding(altarPartBehavior);
            buyButton.SetActive(!altarPartBehavior.isBought);
            progressImage.SetActive(altarPartBehavior.isBought);
        }
    }
}

