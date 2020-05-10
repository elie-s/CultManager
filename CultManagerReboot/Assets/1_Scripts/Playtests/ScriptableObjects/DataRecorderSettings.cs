using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace CultManager
{
    [CreateAssetMenu(menuName = "CultManager/Playtests/Recorder Settings")]
    public class DataRecorderSettings : ScriptableObject
    {
        public string dataFolder = "Data";

        [Header("Game Data")]
        public string gamedataFile = "gamedata";
        public string gamedataExtension = "csv";
        public string gamedataPath => Application.persistentDataPath + "/" + dataFolder + "/" + gamedataFile + "." + gamedataExtension;

        [Header("Gestures Data")]
        public string gesturesFile = "gestures";
        public string gesturesExtension = "csv";
        public string gesturesPath => Application.persistentDataPath + "/" + dataFolder + "/" + gesturesFile + "." + gesturesExtension;

        [Header("Touches Data")]
        public string touchesFile = "touches";
        public string touchExtension = "csv";
        public string touchPath => Application.persistentDataPath + "/" + dataFolder + "/" + touchesFile + "." + touchExtension;

        public void CheckForDirectory()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + dataFolder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + dataFolder);
            }
        }
    }
}

