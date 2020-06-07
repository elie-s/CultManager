using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace CultManager
{
    public class PuzzleSaveManager : MonoBehaviour
    {
        [Header("Data Sets")]
        [SerializeField] private PuzzleSaveData puzzleSaveData = default;
        [Header("Settings")]
        [SerializeField] private PuzzleSaveSettings settings = default;

        public static bool puzzleSaveLoaded;

        private string dataPath => Application.persistentDataPath + "/" + settings.saveFolder + "/" + settings.saveName + "." + settings.saveExtension + settings.version;


        private void Awake()
        {
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                SaveGame();
            }
        }

        public void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveGame();
            }
        }


        [ContextMenu("Save")]
        public void SaveGame()
        {
            PuzzleSave puzzleSave = new PuzzleSave(settings.version, puzzleSaveData);

            if (!Directory.Exists(Application.persistentDataPath + "/" + settings.saveFolder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + settings.saveFolder);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Create(dataPath);
            binaryFormatter.Serialize(saveFile, puzzleSave);
            saveFile.Close();
        }

        [ContextMenu("Load")]
        public bool LoadGame()
        {
            if (!File.Exists(dataPath))
            {
                puzzleSaveLoaded = false;
                return false;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(dataPath, FileMode.Open);
            PuzzleSave puzzleSave = (PuzzleSave)binaryFormatter.Deserialize(saveFile);
            saveFile.Close();

            puzzleSaveData.LoadSave(puzzleSave);

            puzzleSaveLoaded = true;
            return true;
        }
    }
}

