using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CultistManager : MonoBehaviour
{
    [Header("Cult Database")]
    [SerializeField]
    CultistData cultistData;
    public List<GameObject> CultistList;
    //public List<CultistProperties> cultDatabase = new List<CultistProperties>();

    [Header("Spawning Cultists")]
    public AgentSpawner cultSpawner;
    public int totalNumberOfCultists;
    private int cultistsToSpawn;

    [Header("UI Components")]
    public Text fps;
    public Text agents;
    public Text sliderAgents;
    public GameObject devPanel;
    public GameObject statPanel;

    [Header("Faith Components")]
    public Image faithBar;
    public float averageFaith;
    float faithTotal;

    void Start()
    {
        cultSpawner.SpawnCultists(totalNumberOfCultists);
    }

    void Update()
    {
        fps.text = "FPS = " + ((int)(1.0f / Time.smoothDeltaTime)).ToString();
        for (int i = 0; i < CultistList.Count; i++)
        {
            faithTotal += cultistData.CultistPropertiesList[i].faith;
        }
        averageFaith = faithTotal / CultistList.Count;
        faithBar.fillAmount = averageFaith / 100f;
        faithTotal = 0;
    }


    #region Developer Tools
    public void KillAll()
    {
        while (CultistList.Count > 0)
        {

            Destroy(CultistList[CultistList.Count - 1]);

            agents.text = "AGENTS = " + (CultistList.Count - 1).ToString();
            CultistList.RemoveAt(CultistList.Count - 1);
        }
        totalNumberOfCultists = CultistList.Count;
    }

    public void Respawn()
    {
        KillAll();
        cultSpawner.SpawnCultists(cultistsToSpawn);
        totalNumberOfCultists = CultistList.Count;
    }

    public void OnSliderChange(float value)
    {
        cultistsToSpawn = Mathf.RoundToInt((value * 250f));
        sliderAgents.text = cultistsToSpawn.ToString();
    }

    public void EnableDevMode(bool x)
    {
        if (x)
        {
            devPanel.SetActive(true);
            statPanel.SetActive(true);
        }
        else
        {
            devPanel.SetActive(false);
            statPanel.SetActive(false);
        }
    }
    #endregion
}
