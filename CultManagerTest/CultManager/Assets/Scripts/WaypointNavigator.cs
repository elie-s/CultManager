using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    CultistController controller;
    public Waypoint currentWaypoint;
    float direction;

    void Awake()
    {
        controller = GetComponent<CultistController>();
    }

    void Start()
    {
        direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        controller.destination = currentWaypoint.transform;
    }

    void Update()
    {
        if (controller.reachedDestination)
        {
            if (direction == 0)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
            }
            else if (direction == 1)
            {
                currentWaypoint = currentWaypoint.previousWaypoint;
            }
            
            controller.destination = currentWaypoint.transform;
        }
    }
}
