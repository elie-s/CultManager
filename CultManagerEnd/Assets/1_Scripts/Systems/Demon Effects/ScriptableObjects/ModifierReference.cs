using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Demon/ModifierReference")]
    public class ModifierReference : ScriptableObject,ILoadable
    {
        public ModifierStorage storage;

        public void LoadSave(Save _save)
        {
            storage = _save.storage;
        }
    }
}

