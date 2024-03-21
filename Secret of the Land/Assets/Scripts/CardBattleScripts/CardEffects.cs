using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;

namespace CardBattle
{

    public class CardEffects : MonoBehaviour
    {
        public GameObject cardGameManagerObject;
        private CardGameManager cardGameManager;

        private void Start()
        {
            cardGameManager = cardGameManagerObject.GetComponent<CardGameManager>();
        }
        public void Effect_1_Attack()
        {
            cardGameManager.enemy.takenDamage(2);
            Debug.Log("Attack");
        }

        public void Effect_2_Defend()
        {
            cardGameManager.player.addShield(2);
            Debug.Log("defend");
        }

        public void Effect_3_DeadlyStruggle()
        {
            cardGameManager.enemy.takenDamage(2);
            cardGameManager.player.IncreaseEnergy(1);
            Debug.Log("ds");
        }

        public void Effect_4_EnergyBoost()
        {
            cardGameManager.player.IncreaseEnergy(4);
            Debug.Log("EB");
        }


    }
}
