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
            cardGameManager.enemy.AddShield(enemyDefendShield);
            Debug.Log("Enemy defending");
        }



        public Action CodeToAction(int code, ref String actionInformation)
        {
            switch (code)
            {
                case 1:
                    actionInformation = "Enemy will deal " + cardGameManager.enemy.currentDamage + " damage to you in next round";
                    return EnemyAction_1_Attack;
                case 2:
                    actionInformation = "Enemy will defend in next round";
                    return EnemyAction_2_Defend;
                default:
                    throw new Exception("Enemy action not found with code " + code);
            }
        }
    }

}
