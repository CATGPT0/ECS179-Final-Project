using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;

namespace CardBattle
{

    public class Enemy : MonoBehaviour
    {
        public int HP = 10;
        int maxAttackDamage = 5; // Inclusive
        int minAttackDamage = 3; // Inclusive

        public void takenDamage(int damage)
        {
            HP -= damage;

            if (HP <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Destroy(this.gameObject);
        }

    }
}
