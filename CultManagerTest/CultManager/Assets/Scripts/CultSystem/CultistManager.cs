using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistManager : MonoBehaviour
{
    [Header("Cult Database")]
    [SerializeField]
    CultistData cultistData;
    public List<GameObject> CultistList;

    [Header("Spawning Cultists")]
    public AgentSpawner cultSpawner;
    private int cultistsToSpawn;

    float faithTotal;

    void Start()
    {
        cultistData.ResetCultistData();
        cultSpawner.SpawnCultists(cultistData.totalNumberOfCultists);
    }

    void Update()
    {
        for (int i = 0; i < CultistList.Count; i++)
        {
            faithTotal += cultistData.CultistPropertiesList[i].faith;
        }
        cultistData.averageFaith = faithTotal / CultistList.Count;
        faithTotal = 0;
    }

    public void KillAll()
    {
        while (CultistList.Count > 0)
        {
            Destroy(CultistList[CultistList.Count - 1]);
            cultistData.CultistPropertiesList.RemoveAt(CultistList.Count - 1);
            CultistList.RemoveAt(CultistList.Count - 1);
            cultistData.totalNumberOfCultists--;
        }
        //cultistData.totalNumberOfCultists = 0;
    }

    public void Respawn(int number)
    {
        KillAll();
        cultSpawner.SpawnCultists(number);
        //cultistData.totalNumberOfCultists = number;
    }
}
