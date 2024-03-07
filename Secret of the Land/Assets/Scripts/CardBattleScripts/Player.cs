using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardBattle;

namespace CardBattle
{

    public class Player : MonoBehaviour
    {
        public int HP = 10;
        public int energy = 2;
        public int health = 5;

        public bool ReduceEnergy(int amount)
        {
            if (amount > energy)
            {
                return false;
            }
            else
            {
                this.energy -= amount;
                return true;
            }
        }

        public void IncreaseEnergy(int amount)
        {
            this.energy += amount;
        }


        public void takenDamage(int damage)
        {
            this.health -= damage;
        }
    }
}