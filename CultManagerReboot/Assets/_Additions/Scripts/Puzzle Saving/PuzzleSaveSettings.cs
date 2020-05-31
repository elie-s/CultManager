using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Save/PuzzleSaveSettings")]
    public class PuzzleSaveSettings : ScriptableObject
    {
        public int version = 1;
        public string saveFolder = "Save";
        public string saveName = "puzzle";
        public string saveExtension = "puzzleSave";

        public string dataPath => Application.persistentDataPath + "/" + saveFolder + "/" + saveName + "." + saveExtension + version;
    }
}

