using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CultManager
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private DebugInstance debug = default;
        [SerializeField] private CultData data = default;
        [SerializeField] private int version = 0;
        [SerializeField] private string saveFolder = "Save";
        [SerializeField] private string saveName = "save";
        [SerializeField] private string saveExtension = "chtulhu";

        private string dataPath => Application.persistentDataPath + "/" + saveFolder + "/" + saveName + "." + saveExtension + version;

        [ContextMenu("Save")]
        public void SaveGame()
        {
            Save save = new Save(version, data);

            if (!Directory.Exists(Application.persistentDataPath + "/" + saveFolder)) Directory.CreateDirectory(Application.persistentDataPath + "/" + saveFolder);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Create(dataPath);
            binaryFormatter.Serialize(saveFile, save);
            saveFile.Close();

            debug.Log("Game saved at: " + dataPath, DebugInstance.Importance.Average);
        }

        [ContextMenu("Load")]
        public void Loadgame()
        {
            if (!File.Exists(dataPath))
            {
                return;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(dataPath, FileMode.Open);
            Save save = (Save)binaryFormatter.Deserialize(saveFile);
            saveFile.Close();

            debug.Log("Game load from: " + dataPath, DebugInstance.Importance.Average);

            data.LoadSave(save);
        }
    }
}