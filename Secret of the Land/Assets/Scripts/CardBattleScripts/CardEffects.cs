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
        }


    }
}
