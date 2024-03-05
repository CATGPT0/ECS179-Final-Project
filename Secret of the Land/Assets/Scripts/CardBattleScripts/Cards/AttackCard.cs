using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using CardBattle;

namespace CardBattle
{
    public class AttackCard : Card
    {

        private void Start()
        {
            energyCost = 2;
            cardCode = 1;
        }

        public override void Effect()
        {
            enemy.takenDamage(2);
            Debug.Log("Processing Attack Effect");
        }

    }
}
