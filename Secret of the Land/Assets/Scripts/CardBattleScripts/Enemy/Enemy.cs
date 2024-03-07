using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using System;

namespace CardBattle
{

    public class Enemy : MonoBehaviour
    {
        public int HP = 10;
        int maxAttackDamage = 5; // Inclusive
        int minAttackDamage = 3; // Inclusive

        // The Action pattern should be passed from other scene
        // It should be a list of code of actions
        public List<int> enemyActionsPattern;

        /// <summary>
        ///  For Debugging, the enemy will loop attacking and defending 
        /// </summary>
        private void Awake()
        {
            enemyActionsPattern = new List<int> { 1, 2 };
        }

        private void Start()
        {
            
        }
        public void takenDamage(int damage)
        {
            HP -= damage;
        }

        public void Die()
        {
            Destroy(this.gameObject);
        }
    }
}
