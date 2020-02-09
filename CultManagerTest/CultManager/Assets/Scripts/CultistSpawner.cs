using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class CultistSpawner : MonoBehaviour
{
    [Header("Cultist Components")]
    public GameObject cultistPrefab;
    public int cultistsToSpawn;
    public List<GameObject> cultistList;

    [Header("UI Components")]
    public Text fps, agents, sliderAgents;
    public GameObject devPanel;
    public GameObject statPanel;

    [Header("Faith Components")]
    public Image faithBar;
    public float averageFaith;
    float faithTotal;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        fps.text = "FPS = " + ((int)(1.0f / Time.smoothDeltaTime)).ToString();

        for (int i = 0; i < cultistList.Count; i++)
        {
            faithTotal +=cultistList[i].GetComponent<CultistController>().faith;
        }
        averageFaith = faithTotal / cultistList.Count;
        faithBar.fillAmount = averageFaith / 100f;
        faithTotal = 0;
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < cultistsToSpawn)
        {
            GameObject obj = Instantiate(cultistPrefab);
            cultistList.Add(obj);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;

            yield return new WaitForEndOfFrame();

            count++;
            agents.text = "AGENTS = " + count.ToString();
        }

    }
    #region Developer Tools
    public void KillAll()
    {
        while (cultistList.Count > 0)
        {
            
            Destroy(cultistList[cultistList.Count-1]); 

            agents.text = "AGENTS = " + (cultistList.Count-1).ToString();
            cultistList.RemoveAt(cultistList.Count - 1);
        }
    }

    public void Respawn()
    {
        KillAll();
        StartCoroutine(Spawn());
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

    #region Faith Bar


    #endregion
}
