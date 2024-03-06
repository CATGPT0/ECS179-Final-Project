using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using System;

namespace CardBattle {

    public class CardBattleManager : MonoBehaviour
    {
        public Action EffectDelegate;

        private CardEffects cardEffects;

        private void Start()
        {
            cardEffects = this.gameObject.GetComponent<CardEffects>();
        }

        /// <summary>
        /// Will do the effect of the card.
        /// </summary>
        /// <param name="cardCode">
        /// The code of the card.
        /// </param>
        public void ProcessCardEffect(int cardCode) 
        {

            switch (cardCode)
            {
                // Attack card
                case 1:
                    EffectDelegate = cardEffects.Effect_1_Attack;
                    break;
                default:
                    throw new Exception("Card effect does not find with code " + cardCode);
            }

            if (EffectDelegate == null)
            {
                throw new Exception("Delegate EffectDelegate in CardBattleManger is null");
            }

            // Process the effect
            EffectDelegate();
        }

        


    }
}
