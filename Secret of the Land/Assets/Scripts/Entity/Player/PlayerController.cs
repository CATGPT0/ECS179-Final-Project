using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using UnityEngine.AI;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private SoundEffectController soundEffectController;
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
       
        private NavMeshAgent agent;

        void Awake()
        {
            player = GetComponentInChildren<Player>();
            battleController = GetComponentInChildren<BattleController>(); 
            levelManager = GetComponentInChildren<LevelManager>();
            playerAnimation = GetComponentInChildren<PlayerAnimation>();
            playerEvent = GetComponentInChildren<PlayerEvent>();
            agent = GetComponent<NavMeshAgent>();
            agent.updateUpAxis = false;
            agent.updateRotation = false;
        }

        void Start()
        {
            battleController.transform.position = this.transform.position;
        }

        void Update()
        {
            Player.properties.VelocityX = Input.GetAxisRaw("Horizontal");
            Player.properties.VelocityY = Input.GetAxisRaw("Vertical");
            Move();
        }

        void Move()
        {
            Player.properties.Direction = new Vector3(Player.properties.VelocityX, Player.properties.VelocityY, 0);

            if (Player.properties.Direction != Vector3.zero)
            {
                Player.properties.LatestMoveDirection = Player.properties.Direction;
            }

            this.transform.position += GetCalculatedSpeed() * Time.deltaTime * Player.properties.Direction;
        }

        ///<summary>
        ///Get the calculated speed of the player
        ///</summary>
        ///<returns> The calculated speed of the player </returns>
        private float GetCalculatedSpeed()
        {
            if (Player.properties.VelocityX != 0 && Player.properties.VelocityY != 0)
            {
                return player.properties.Speed / Mathf.Sqrt(2);
            }
            else
            {
                return player.properties.Speed;
            }
        }
        public void Attack()
        {
            battleController.Attack();
            //soundEffectController.PlayAttackSound();
        }

        public void StopAttack()
        {
            soundEffectController.StopAttackSound();
        }
    }
}
