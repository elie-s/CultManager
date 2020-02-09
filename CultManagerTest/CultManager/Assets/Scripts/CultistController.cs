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

    [Header("Cultist Properties")]
    public float faith;

    public bool isMale;

    void Awake()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        faith = Mathf.RoundToInt(Random.Range(0, 100f));
        isMale = Mathf.RoundToInt(Random.Range(0f, 1f)) == 1 ? true : false;
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
