using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevToolsManager : MonoBehaviour
{

    [Header("Cult Database")]
    [SerializeField]
    CultistData cultistData;

    [Header("Cult Management")]
    [SerializeField]
    CultistManager cultistManager;
    private int cultistsToSpawn;

    [Header("UI Components")]
    public Text fps;
    public Text agents;
    public Text sliderAgents;
    public GameObject devPanel;
    public GameObject statPanel;

    private void Start()
    {
        agents.text = "AGENTS = " + cultistData.totalNumberOfCultists.ToString();
    }

    void Update()
    {
        UpdateFPS(); 
    }

    void UpdateFPS()
    {
        fps.text = "FPS = " + ((int)(1.0f / Time.smoothDeltaTime)).ToString();
    }

    #region Developer Tools

    public void RespawnAgents()
    {
        cultistManager.Respawn(cultistsToSpawn);
        agents.text = "AGENTS = " + cultistsToSpawn.ToString();
    }

    public void KillAllAgents()
    {
        cultistManager.KillAll();
        agents.text = "AGENTS = " + 0;
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
