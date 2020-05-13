using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Save/SaveSettings")]
    public class SaveSettings : ScriptableObject
    {
        public int version = 1;
        public string saveFolder = "Save";
        public string saveName = "save";
        public string saveExtension = "chtulhu";

        public string dataPath => Application.persistentDataPath + "/" + saveFolder + "/" + saveName + "." + saveExtension + version;
    }
}

