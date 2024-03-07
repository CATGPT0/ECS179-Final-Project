using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using System;

namespace CardBattle
{
    public class EnemyActions : MonoBehaviour
    {
        public GameObject EnemyGameObject;
        private Enemy enemy;
        public void EnemyAction_1_Attack()
        {
            Debug.Log("Enemy Attacking");
        }

        public void EnemyAction_2_Defend()
        {
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
