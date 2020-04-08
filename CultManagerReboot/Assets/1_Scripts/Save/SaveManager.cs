﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace CultManager
{
    public class SaveManager : MonoBehaviour
    {
        [Header("Data Sets")]
        [SerializeField] private CultData cultData = default;
        [SerializeField] private InfluenceData influenceData = default;
        [SerializeField] private MoneyData moneyData = default;
        [SerializeField] private PoliceData policeData = default;
        [SerializeField] private AltarData altarData = default;
        [Header("Settings")]
        [SerializeField] private SaveSettings settings = default;

        public static bool saveLoaded;

        private string dataPath => Application.persistentDataPath + "/" + settings.saveFolder + "/" + settings.saveName + "." + settings.saveExtension + settings.version;

        [ContextMenu("Save")]
        public void SaveGame()
        {
            Save save = new Save(settings.version, cultData, influenceData, moneyData, policeData,altarData);

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

            cultData.LoadSave(save);
            influenceData.LoadSave(save);
            moneyData.LoadSave(save);
            policeData.LoadSave(save);
            altarData.LoadSave(save);

            saveLoaded = true;
            return true;
        }
    }
}
