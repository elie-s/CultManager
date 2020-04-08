using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace CultManager
{
    public class CardDisplay : MonoBehaviour
    {
        [Header("Display")]
        public Image image;
        public Text nameText;
        public Text ageText;
        public Text moneyText;
        public Text policeText;

        //[ContextMenu("Display")]
        public void Display(Sprite _image,string _cultistName, int _cultistAge, int _policeValue, int _moneyValue)
        {
            image.sprite = _image;
            nameText.text = _cultistName;
            ageText.text = _cultistAge.ToString();
            moneyText.text = _moneyValue.ToString();
            policeText.text = _policeValue.ToString();
        }
    }
}

