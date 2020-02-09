using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class CultistSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public int pedestriansToSpawn;
    public Text fps, agents;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        fps.text ="FPS = "+((int)(1.0f / Time.smoothDeltaTime)).ToString();
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < pedestriansToSpawn)
        {
            GameObject obj = Instantiate(pedestrianPrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            obj.transform.position = child.position;
            
            yield return new WaitForEndOfFrame();

            count++;
            agents.text = "AGENTS = " + count.ToString();
        }
        
    }
}
