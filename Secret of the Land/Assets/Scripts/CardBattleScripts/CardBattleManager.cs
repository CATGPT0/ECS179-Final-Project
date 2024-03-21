using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using System;

namespace CardBattle {

    public class CardBattleManager : MonoBehaviour
    {
        public Action CardEffectDelegate;

        private CardEffects cardEffects;

        private EnemyActions enemyActions;

        public Action enemyActionDelegate;

        public String enemyNextActionInformation = "None";

        // counter for decide which action in counter should be used
        public int enemyActionCounter;

        private void Awake()
        {
            cardEffects = this.gameObject.GetComponent<CardEffects>();
            enemyActions = this.gameObject.GetComponent<EnemyActions>();
            enemyActionCounter = 0;
        }

        /// <summary>
        /// Will do the effect of the card.
        /// </summary>
        /// <param name="cardCode">
        /// The code of the card.
        /// </param>
        public void ProcessCardEffect(int cardCode) 
        {
            Debug.Log("Processing" + cardCode);
            // Set delegate
            switch (cardCode)
            {
                // Attack card
                case 1:
                    CardEffectDelegate = cardEffects.Effect_1_Attack;
                    break;
                case 2:
                    CardEffectDelegate = cardEffects.Effect_2_Defend;
                    break;
                case 3:
                    CardEffectDelegate = cardEffects.Effect_3_DeadlyStruggle;
                    break;
                case 4:
                    CardEffectDelegate = cardEffects.Effect_4_EnergyBoost;
                    break;
                default:
                    throw new Exception("Card effect does not find with code " + cardCode);
            }

            if (CardEffectDelegate == null)
            {
                throw new Exception("Delegate EffectDelegate in CardBattleManger is null");
            }

            // Process the effect
            CardEffectDelegate();
        }



        // Enemy
        public void DoEnemyAction()
        {
            enemyActionDelegate();
        }

        public void LoadNextEnemyAction(Enemy enemy)
        {
            // use counter to decide which code we should use in enemyActions
            enemyActionCounter = enemyActionCounter % enemy.enemyActionsPattern.Count;
            int code = enemy.enemyActionsPattern[enemyActionCounter];

            // Code to Action
            enemyActionDelegate = enemyActions.CodeToAction(code, ref enemyNextActionInformation);

            // Increase counter
            enemyActionCounter++;
        }
        


    }
}
