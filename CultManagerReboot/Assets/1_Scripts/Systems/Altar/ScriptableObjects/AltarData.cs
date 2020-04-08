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

        public void LoadSave(Save _save)
        {
            SetAvailableCultists(_save.availableCultists);
        }
    }
}

