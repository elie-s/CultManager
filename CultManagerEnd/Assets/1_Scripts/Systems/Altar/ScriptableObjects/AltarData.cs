using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Systems/AltarData")]
    public class AltarData : ScriptableObject
    {
        public List<AltarPart> altarParts;
        public int availableCultists;
        public bool altarCompletion;
        public DateTime lastBuildTimeReference;


        public void Reset()
        {
            altarParts = new List<AltarPart>();
            availableCultists = 0;
            altarCompletion = false;
        }

        public void ResetAltarData()
        {
            Reset();
        }

        public AltarPart CreateNewAltarPart(string name)
        {
            AltarPart current = new AltarPart(name);

            return current;
        }

        public void BreakAltarPart(AltarPart part)
        {
            part.SetBuildPoints();
        }

        public void AddAltarPart(AltarPart altarPart)
        {
            altarParts.Add(altarPart);
            Debug.Log("Name " + altarPart.altarPartName);
        }

        public void RemoveAltarPart(AltarPart altarPart)
        {
            altarParts.Remove(altarPart);
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
            for (int i = 0; i < altarPartBehaviors.Length; i++)
            {
                altarParts.Add(altarPartBehaviors[i].altarPart);
            }
        }

        public void ResetBuildTimeReference()
        {
            lastBuildTimeReference = System.DateTime.Now;
        }

        public void ResetBuildTimeReference(DateTime _dateTime)
        {
            lastBuildTimeReference = _dateTime;
        }

        //public void LoadSave(Save _save)
        //{
        //    SetAvailableCultists(_save.availableCultists);
        //    altarParts = _save.altarParts.ToList();
        //    ResetBuildTimeReference(_save.lastBuildTimeReference);
        //}
    }
}

