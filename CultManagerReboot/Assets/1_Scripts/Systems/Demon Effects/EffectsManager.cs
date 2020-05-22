using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0414
namespace CultManager
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private DemonEffects DemonEffects = default;
        [SerializeField] private ModifierReference reference = default;
        [SerializeField] private DemonData demonData = default;

        [SerializeField] private PoliceManager police = default;
        [SerializeField] private InfluenceManager influence = default;
        [SerializeField] private MoneyManager money = default;
        [SerializeField] private CultManager recruitment = default;


        Dictionary<EffectType, float> dictonary = new Dictionary<EffectType, float>();

        private void Awake()
        {
            InitDict();
        }

        private void InitDict()
        {
            for (int i = 0; i < 10; i++)
            {
                dictonary.Add((EffectType)i, 0.0f);
            }
        }

        public void UpdateModifiers()
        {
            List<Modifier> all = new List<Modifier>();
            for (int i = 0; i < demonData.spawns.Count; i++)
            {
                for (int j = 0; j < demonData.spawns[i].modifiers.Length; j++)
                {
                    all.Add(demonData.spawns[i].modifiers[j]);
                }
            }
            UpdateEffects(all.ToArray());

        }

        public void UpdateEffects(Modifier[] modifiers)
        {
            for (int i = 0; i < modifiers.Length; i++)
            {
                dictonary[modifiers[i].effect] += modifiers[i].value;
            }
            UpdateValues();
        }

        public void UpdateValues()
        {
            reference.storage.PoliceDecrementModifier = dictonary[EffectType.PoliceDecrementModifier];
            reference.storage.PoliceIncrementModifier = dictonary[EffectType.PoliceIncrementModifier];
            reference.storage.PoliceBribeModifier = dictonary[EffectType.PoliceBribeModifier];

            reference.storage.InfluenceDecrementModifier = dictonary[EffectType.InfluenceDecrementModifier];
            reference.storage.InfluenceIncrementModifier = dictonary[EffectType.InfluenceIncrementModifier];

            reference.storage.MoneyDecrementModifier = dictonary[EffectType.MoneyDecrementModifier];
            reference.storage.MoneyIncrementModifier = dictonary[EffectType.MoneyIncrementModifier];

            reference.storage.RecruitmentQueueModifier = dictonary[EffectType.RecruitmentQueueModifier];
            reference.storage.RecruitmentMoneyModifier = dictonary[EffectType.RecruitmentMoneyModifier];
            reference.storage.RecruitmentPoliceModifier = dictonary[EffectType.RecruitmentPoliceModifier];
        }

        public Modifier[] SetRandomSpawnEffect(int length,int patternSegments)
        {
            Modifier[] modifiers = new Modifier[length];
            for (int i = 0; i < length-patternSegments; i++)
            {
                modifiers[i] = new Modifier();
                modifiers[i] = DemonEffects.SegmentModifiers[Random.Range(0, DemonEffects.SegmentModifiers.Length)];
            }
            for (int i = length-patternSegments ; i < length; i++)
            {
                modifiers[i] = new Modifier();
                modifiers[i] = DemonEffects.PatternSegmentModifiers[Random.Range(0, DemonEffects.PatternSegmentModifiers.Length)];
            }
            return modifiers;
        }

        public Modifier[] SetRandomPersistentEffect(int length)
        {
            Modifier[] modifiers = new Modifier[length];
            for (int i = 0; i < length; i++)
            {
                modifiers[i] = new Modifier();
                modifiers[i] = DemonEffects.PatternSegmentModifiers[Random.Range(0, DemonEffects.PatternSegmentModifiers.Length)];
            }
            return modifiers;
        }

    }
}

