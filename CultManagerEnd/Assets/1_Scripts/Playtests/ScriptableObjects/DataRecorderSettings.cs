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
        public int currentSession = 0;
        public string testerName = "";

        public string folder => Application.persistentDataPath + "/" + dataFolder + "/" + currentSession;

        [Header("Game Data")]
        public string gamedataFile = "gamedata";
        public string gamedataExtension = "csv";
        public string gamedataPath => Application.persistentDataPath + "/" + dataFolder + "/" + currentSession + "/" + testerName + "-" + gamedataFile + "." + gamedataExtension;

        [Header("Gestures Data")]
        public string gesturesFile = "gestures";
        public string gesturesExtension = "csv";
        public string gesturesPath => Application.persistentDataPath + "/" + dataFolder + "/" + currentSession + "/" + testerName + "-" + gesturesFile + "." + gesturesExtension;

        [Header("Touches Data")]
        public string touchesFile = "touches";
        public string touchExtension = "csv";
        public string touchPath => Application.persistentDataPath + "/" + dataFolder + "/" + currentSession + "/" + testerName + "-" + touchesFile + "." + touchExtension;

        [Header("Touches Data")]
        public string quickFeedbacksFile = "qfb";
        public string quickFeedbacksExtension = "tsv";
        public string quickFeedbacksPath => Application.persistentDataPath + "/" + dataFolder + "/" + currentSession + "/" + testerName + "-" + quickFeedbacksFile + "." + quickFeedbacksExtension;

        public void CheckForDirectory()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + dataFolder + "/" + currentSession))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + dataFolder + "/" + currentSession);
            }
        }

        public void LoadSave(Save _save)
        {
            //currentSession = _save.sessionCount;
            //testerName = _save.testerName;
        }
    }
}

