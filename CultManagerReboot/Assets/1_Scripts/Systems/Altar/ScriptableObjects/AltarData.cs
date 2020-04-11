using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Systems/AltarData")]
    public class AltarData : ScriptableObject
    {
        public AltarPart[] altarParts;
        public int availableCultists;
        public bool altarCompletion;

        public void Reset()
        {
            for (int i = 0; i < altarParts.Length; i++)
            {
                altarParts[i] = new AltarPart();
            }
            availableCultists = 0;
            altarCompletion = false;
        }

        public void IncreaseAvailableCultists(int _value)
        {
            availableCultists += _value;
        }

        public void DecreaseAvailableCultists(int _value)
        {
            availableCultists -= _value;
        }

        public void SetAvailableCultists(int _value)
        {
            availableCultists = _value;
        }

        public void SetAvailableCultists()
        {
            availableCultists = 0;
        }

        public void SetAltarParts(AltarPartBehavior[] altarPartBehaviors)
        {
            altarParts = new AltarPart[altarPartBehaviors.Length];
            for (int i = 0; i < altarPartBehaviors.Length; i++)
            {
                altarParts[i] = altarPartBehaviors[i].altarPart;
            }
        }


        public void LoadSave(Save _save)
        {
            SetAvailableCultists(_save.availableCultists);
            altarParts = _save.altarParts;
            Debug.Log(_save.altarParts.Length);
        }
    }
}

