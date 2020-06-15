﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CultManager
{
    public class SpawnBehavior : MonoBehaviour
    {
        public Spawn spawn;
        public DemonManager manager;
        public FadeBehavior spawnFade;
        public DemonData data=default;

        [SerializeField] private SpriteRenderer alpha = default;
        [SerializeField] private Gradient accuracyColor = default;
        

        public bool canMove;
        [SerializeField] private float timeInterval = default;
        [SerializeField] private float speed = default;
        [SerializeField] private BoxCollider2D area = default;

        [SerializeField] private Vector3 movePosition = default;
        private bool dead;
        private float waitTime;


        DemonBookUI demonBook;
        CameraController camControl;


        private void Update()
        {
            if (spawn.CheckDeath() && !dead)
            {
                dead = true;
                manager.ReturnLoot(spawn);
                manager.KillSpawn(spawn);
            }
            if (canMove)
            {
                Move();
            }
        }

        public void Init(Spawn _spawn,DemonManager _manager,float accuracy,BoxCollider2D _area,DemonBookUI _demonBook)
        {
            camControl = FindObjectOfType<CameraController>();
            spawn = _spawn;
            manager = _manager;
            area = _area;
            demonBook = _demonBook;
            ColorIt(accuracy);
            movePosition = new Vector2(Random.Range(area.bounds.min.x, area.bounds.max.x), Random.Range(area.bounds.min.y, area.bounds.max.y));
        }

        public void EnableMove()
        {
            canMove = true;
        }

        public void ZoomOut()
        {
            camControl.TransitionToOrigin();
            Invoke("DisplayDemonPage", 1f);
        }

        public void DisplayDemonPage()
        {
            demonBook.Open();
            demonBook.DisplayThisDemon(data.demons.Count - 1);
        }

        

        public void ColorIt(float accuracy)
        {
            alpha.color = accuracyColor.Evaluate(accuracy);
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
