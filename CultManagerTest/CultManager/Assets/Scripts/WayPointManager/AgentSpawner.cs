using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class AgentSpawner : MonoBehaviour
{
    [Header("Cultist Components")]
    public CultistData cultistData;
    public CultistManager cultistManager;
    public bool OnStartSpawn;
    [SerializeField]
    private int cultistsToSpawn;
    public Transform LastZoneTransform;

    void Start()
    {
        if (OnStartSpawn)
        {
            StartCoroutine(Spawn());
        }
    }

    public void SpawnCultists(int number)
    {
        cultistsToSpawn = number;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < cultistsToSpawn)
        {
            GameObject obj = Instantiate(cultistData.cultistPrefab,gameObject.transform);
            CultistProperties prop = obj.GetComponent<CultistController>().cultistProperties;
            cultistManager.CultistList.Add(obj);
            cultistData.CultistPropertiesList.Add(prop);
            cultistData.totalNumberOfCultists++;
            
            Transform child = transform.GetChild(Random.Range(0, LastZoneTransform.GetSiblingIndex()));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;

            yield return new WaitForEndOfFrame();

            count++;
        }

    }

}
