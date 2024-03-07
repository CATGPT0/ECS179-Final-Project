using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using System;

namespace CardBattle
{
    public class EnemyActions : MonoBehaviour
    {
        public GameObject cardGameMangerObject;
        private CardGameManager cardGameManager;

        private void Awake()
        {
            cardGameManager = cardGameMangerObject.GetComponent<CardGameManager>();
        }
        public void EnemyAction_1_Attack()
        {
            int enemyDamage = cardGameManager.enemy.currentDamage;
            cardGameManager.player.TakenDamage(enemyDamage);
            Debug.Log("Enemy Attacking");
        }

        public void EnemyAction_2_Defend()
        {
            int enemyDefendShield = UnityEngine.Random.Range
                (cardGameManager.enemy.minShield, cardGameManager.enemy.maxShield);
            Debug.Log("Enemy defending");
        }



        public Action CodeToAction(int code)
        {
            switch (code)
            {
                case 1:
                    return EnemyAction_1_Attack;
                case 2:
                    return EnemyAction_2_Defend;
                default:
                    throw new Exception("Enemy action not found with code " + code);
            }
        }
    }

}
