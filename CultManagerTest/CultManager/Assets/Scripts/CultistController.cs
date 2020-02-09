using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistController : MonoBehaviour
{
    [Range(1f,3f)]
    public float maxSpeed;
    [Range(3f, 5f)]
    public float minSpeed;
    [SerializeField]
    float moveSpeed;
    public Transform destination;
    public bool reachedDestination;

    void Awake()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        if (destination)
        {
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(destination.position.x, destination.position.z)) <= 1f)
            {
                reachedDestination = true;
            }
            else
            {
                Move();
            }
        }
        
        
    }

    void Move()
    {
        reachedDestination = false;
        transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
    }
}
