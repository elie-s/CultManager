using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CultistProperties")]
public class CultistData : ScriptableObject
{
    public GameObject cultistPrefab;
    public int maxNumberOfCultists;
    public int totalNumberOfCultists;
    public float maxFaith;
    public float averageFaith;
    public List<CultistProperties> CultistPropertiesList;

    public void ResetCultistData()
    {
        totalNumberOfCultists = 0;
        averageFaith = 0;
        CultistPropertiesList.Clear();
    }
}
