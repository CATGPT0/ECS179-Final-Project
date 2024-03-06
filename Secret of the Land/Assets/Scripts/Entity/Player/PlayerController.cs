using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        private BattleController battleController;
        public BattleController BattleController
        {
            get { return battleController; }
            set { battleController = value; }
        }
        private LevelManager levelManager;
        public LevelManager LevelManager
        {
            get { return levelManager; }
            set { levelManager = value; }
        }
        private PlayerAnimation playerAnimation;
        public PlayerAnimation PlayerAnimation
        {
            get { return playerAnimation; }
            set { playerAnimation = value; }
        }
        private PlayerEvent playerEvent;
        public PlayerEvent PlayerEvent
        {
            get { return playerEvent; }
            set { playerEvent = value; }
        }
        private float velocityX;
        public float VelocityX
        {
            get { return velocityX; }
            set { velocityX = value; }
        }

        private float velocityY;
        public float VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }

        private Vector3 direction;
        public Vector3 Direction
        {
            get { return direction; }
            private set { direction = value; }
        }

        private Vector3 latestMoveDirection;
        public Vector3 LatestMoveDirection
        {
            get { return latestMoveDirection; }
            private set { latestMoveDirection = value; }
        }

        void Awake()
        {
            player = GetComponentInChildren<Player>();
            battleController = GetComponentInChildren<BattleController>(); 
            levelManager = GetComponentInChildren<LevelManager>();
            playerAnimation = GetComponentInChildren<PlayerAnimation>();
            playerEvent = GetComponentInChildren<PlayerEvent>();
        }

        void Start()
        {
            battleController.transform.position = this.transform.position;
        }

        void Update()
        {
            velocityX = Input.GetAxisRaw("Horizontal");
            velocityY = Input.GetAxisRaw("Vertical");
            Move();
        }

        void Move()
        {
            this.direction = new Vector3(velocityX, velocityY, 0);

            if (direction != Vector3.zero)
            {
                latestMoveDirection = direction;
            }

            this.transform.position += GetCalculatedSpeed() * Time.deltaTime * direction;
        }

        ///<summary>
        ///Get the calculated speed of the player
        ///</summary>
        ///<returns> The calculated speed of the player </returns>
        private float GetCalculatedSpeed()
        {
            if (velocityX != 0 && velocityY != 0)
            {
                return player.Speed / Mathf.Sqrt(2);
            }
            else
            {
                return player.Speed;
            }
        }
        public void Attack()
        {
            battleController.Attack();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Grass"))
            {
                Debug.Log("Grass");
            }
        }
    }
}
