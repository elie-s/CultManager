using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace CultManager
{
    public class SaveManager : MonoBehaviour
    {
        [Header("Data Sets")]
        [SerializeField] private NoteTabData noteTabData = default;
        [SerializeField] private PuzzleData puzzleData = default;
        [SerializeField] private BloodBankData bloodBankData = default;
        [SerializeField] private DemonData demonData = default;
        [SerializeField] private PersistentDemonData persistentDemonData = default;
        [SerializeField] private ModifierReference modifierReference = default;
        [Header("Settings")]
        [SerializeField] private SaveSettings settings = default;

        [SerializeField] public int currentCultistsDebug = 0;
        [SerializeField] public int currentMoneyDebug = 0;
        [SerializeField] public int currentInfluenceDebug = 0;
        [SerializeField] public int currentPoliceDebug = 0;

        public static bool saveLoaded;

        private string dataPath => Application.persistentDataPath + "/" + settings.saveFolder + "/" + settings.saveName + "." + settings.saveExtension + settings.version;


        [ContextMenu("Save")]
        public void SaveGame()
        {
            Save save = new Save(settings.version, noteTabData, puzzleData, bloodBankData,demonData, persistentDemonData,modifierReference);

            if (!Directory.Exists(Application.persistentDataPath + "/" + settings.saveFolder))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + settings.saveFolder);
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Create(dataPath);
            binaryFormatter.Serialize(saveFile, save);
            saveFile.Close();
        }

        [ContextMenu("Load")]
        public bool Loadgame()
        {
            if (!File.Exists(dataPath))
            {
                saveLoaded = false;
                return false;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream saveFile = File.Open(dataPath, FileMode.Open);
            Save save = (Save)binaryFormatter.Deserialize(saveFile);
            saveFile.Close();

            noteTabData.LoadSave(save);
            puzzleData.LoadSave(save);
            bloodBankData.LoadSave(save);
            demonData.LoadSave(save);
            persistentDemonData.LoadSave(save);
            modifierReference.LoadSave(save);

            saveLoaded = true;
            return true;
        }
    }
}

