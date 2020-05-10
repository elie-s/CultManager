using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CultManager
{
    public class DataRecorder : MonoBehaviour
    {
        [SerializeField] private MoneyData money = default;
        [SerializeField] private BloodBankData blood = default;
        [SerializeField] private InfluenceData influence = default;
        [SerializeField] private PoliceData police = default;
        [SerializeField] private CultData cult = default;
        [SerializeField] private DemonData demons = default;
        [SerializeField] private string separation = ";";
        [SerializeField, DrawScriptable] private DataRecorderSettings settings = default;

        public static string Separation;

        private List<GestureRecord> gestures = new List<GestureRecord>();
        private List<TouchRecord> touches = new List<TouchRecord>();
        private List<GameDataSet> gamedata = new List<GameDataSet>();

        private void Awake()
        {
            Separation = separation == "TAB" ? "\t" : separation;
        }

        private void OnEnable()
        {
            gamedata = new List<GameDataSet>();
            InitGesturesRecording();
        }

        private void OnDisable()
        {
            StopAllCoroutines();

            
        }

        // Update is called once per frame
        void Update()
        {
            RecordGameData();
        }

        [ContextMenu("Save Data")]
        public void SaveData()
        {

            settings.CheckForDirectory();
            SaveGesturesData(settings.gesturesPath);

            SaveGamedata(settings.gamedataPath);
            
            SaveTouchesData(settings.touchPath);
        }

        public void SaveGamedata(string _path)
        {
            string[] data = GameDataToString();

            if (File.Exists(_path)) File.Delete(_path);

            File.WriteAllLines(_path, data);
        }

        public void SaveGesturesData(string _path)
        {
            string data = GesturesDataToString();

            //Debug.Log(data);

            if (File.Exists(_path)) File.Delete(_path);

            File.WriteAllText(_path, data);
        }

        public void SaveTouchesData(string _path)
        {
            string[] data = TouchesDataToString();

            if (File.Exists(_path)) File.Delete(_path);

            File.WriteAllLines(_path, data);
        }

        private string[] GameDataToString()
        {
            List<string> result = new List<string>();
            List<GameDataSet> data = gamedata;

            foreach (GameDataSet dataSet in data)
            {
                result.Add(dataSet.ToString());
            }

            return result.ToArray();
        }

        private void RecordGameData()
        {
            if (gamedata == null) gamedata = new List<GameDataSet>();

            GameDataSet current = new GameDataSet(money, blood, influence, police, cult, demons);

            if (gamedata.Count == 0 || GameDataSet.ValueChanged(current, gamedata[gamedata.Count-1])) gamedata.Add(current);
        }

        private List<GameDataSet> Purge()
        {
            List<GameDataSet> result = new List<GameDataSet>();

            result.Add(gamedata[0]);

            for (int i = 1; i < gamedata.Count-1; i++)
            {
                if (GameDataSet.ValueChanged(result[result.Count - 1], gamedata[i])) result.Add(gamedata[i]);
            }

            result.Add(gamedata[gamedata.Count - 1]);

            return result;
        }

        private void InitGesturesRecording()
        {
            gestures = new List<GestureRecord>();
            touches = new List<TouchRecord>();

            StartCoroutine(GestureRecordRoutine());
            StartCoroutine(TouchRecordRoutine());
        }

        private IEnumerator GestureRecordRoutine()
        {
            while (!Gesture.Touching)
            {
                yield return null;
            }

            List<GesturesDataRecord> records = new List<GesturesDataRecord>();

            while (Gesture.Touching)
            {
                records.Add(GesturesDataRecord.current);
                yield return null;
            }

            gestures.Add(new GestureRecord(records));

            StartCoroutine(GestureRecordRoutine());
        }

        private IEnumerator TouchRecordRoutine()
        {

            while(Input.touchCount == 0)
            {
                yield return null;
            }

            List<TouchData>[] data = new List<TouchData>[11];

            for (int i = 0; i < 11; i++)
            {
                data[i] = new List<TouchData>();
            }

            while (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    data[touch.fingerId].Add(new TouchData(touch));
                }
            }

            foreach (List<TouchData> touchDatas in data)
            {
                if (touchDatas.Count > 0) touches.Add(new TouchRecord(touchDatas));
            }

            StartCoroutine(TouchRecordRoutine());
        }

        public string GesturesDataToString()
        {
            string result = "";

            for (int i = 0; i < gestures.Count; i++)
            {
                string[] data = gestures[i].DataToString(i);

                for (int j = 0; j < data.Length; j++)
                {
                    result += data[j];
                    if (i != gestures.Count - 1 || j != data.Length - 1) result += "\n";
                }
            }

            //Debug.Log("all gestures: " + result.Count);

            return result;
        }

        //public string[] GesturesDataToString()
        //{
        //    List<string> result = new List<string>();

        //    for (int i = 0; i < gestures.Count; i++)
        //    {
        //        string[] data = gestures[i].DataToString(i);
        //        foreach (string line in data)
        //        {
        //            result.Add(line);
        //            Debug.Log(line);
        //        }
        //    }

        //    //Debug.Log("all gestures: " + result.Count);

        //    return result.ToArray();
        //}

        public string[] TouchesDataToString()
        {
            List<string> result = new List<string>();

            for (int i = 0; i < touches.Count; i++)
            {
                result.AddRange(touches[i].DataToString(i));
            }

            return result.ToArray();
        }
    }
}