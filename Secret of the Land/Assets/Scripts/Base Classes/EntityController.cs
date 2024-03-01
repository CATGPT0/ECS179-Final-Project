using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using System;

namespace Controller
{
    public class EntityController : MonoBehaviour
    {
        [SerializeField]
        private int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        [SerializeField]
        private int attackPower;
        public int AttackPower
        {
            get { return attackPower; }
            set { attackPower = value; }
        }
        [SerializeField]
        private int armor;
        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }
        [SerializeField]
        private int magicResist;
        public int MagicResist
        {
            get { return magicResist; }
            set { magicResist = value; }
        }
        
        protected EventManager eventManager;

        protected void Init()
        {
            eventManager = FindObjectOfType<EventManager>();
            eventManager.deathEvent.Add(this.gameObject, new List<Action>());
        }

        public void DeactivateSelf()
        {
            this.enabled = false;
        }

        public virtual void DestroySelf()
        {
            eventManager.deathEvent[this.gameObject].ForEach(action => action());
            eventManager.deathEvent.Remove(this.gameObject);
            Destroy(gameObject);
        }

        void Awake()
        {
            Init();
        }
        void Start()
        {
            
        }
        void Update()
        {
            
        }
    }
}
