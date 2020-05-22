using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class PersistentDemonBehavior : MonoBehaviour
    {
        public PersistentDemon persistent;
        public DemonManager manager;
        public FadeBehavior spawnFade;

        public bool canMove;
        [SerializeField] private float timeInterval;
        [SerializeField] private float speed;
        [SerializeField] private BoxCollider2D area;
        [SerializeField] private Vector3 movePosition;
        private bool dead;
        private float waitTime;


        private void Update()
        {
            if (canMove)
            {
                Move();
            }

        }

        public void Init(PersistentDemon _persistent, DemonManager _manager,BoxCollider2D _area)
        {
            persistent = _persistent;
            manager = _manager;
            area = _area;
            movePosition = new Vector2(Random.Range(area.bounds.min.x, area.bounds.max.x), Random.Range(area.bounds.min.y, area.bounds.max.y));
        }

        public void EnableMove()
        {
            canMove = true;
        }

        public void ResetPuzzle()
        {
            manager.ResetPuzzle();
        }

        void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, movePosition) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    movePosition = new Vector2(Random.Range(area.bounds.min.x, area.bounds.max.x), Random.Range(area.bounds.min.y, area.bounds.max.y));
                    waitTime = timeInterval;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }
}

