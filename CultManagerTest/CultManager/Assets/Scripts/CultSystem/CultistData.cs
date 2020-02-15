using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CultistProperties")]
public class CultistData : ScriptableObject
{
    public GameObject cultistPrefab;
    public int maxNumberOfCultists;
    public float maxFaith;
    public List<CultistProperties> CultistPropertiesList;

}
