using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    public class CultistController : MonoBehaviour
    {
        [SerializeField,Range(1f, 3f)]
        private float maxSpeed;
        [SerializeField,Range(3f, 5f)]
        private float minSpeed;
        [SerializeField]
        float moveSpeed;
        public Transform destination;
        public bool reachedDestination;

        private Cultist cultist;
    //public int cultistId;
    /*public int cultistBuilding;
    public int cultistAge;
    public float cultistFaith;*/
    //public CultistProperties cultistProperties;

        void Awake()
        {
            
            /*cultistFaith = Mathf.RoundToInt(Random.Range(0, 100f));
            cultistAge = Mathf.RoundToInt(Random.Range(1, 85f));
            cultistProperties = new CultistProperties(0, 0, "apple", cultistAge, cultistFaith);*/
        }

        public void Init(Cultist _cultist)
        {
            cultist = _cultist;
        }

        private void OnEnable()
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
}

