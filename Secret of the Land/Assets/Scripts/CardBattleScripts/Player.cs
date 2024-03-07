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
        public int shield = 0;

        /// <summary>
        /// Reduce player energy
        /// </summary>
        /// <param name="amount">
        /// Energy Amount
        /// </param>
        /// <returns>
        /// Return true if player energy is reduced
        /// </returns>
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

        /// <summary>
        /// Increase the player energy
        /// </summary>
        /// <param name="amount">
        /// energy amount
        /// </param>
        public void IncreaseEnergy(int amount)
        {
            this.energy += amount;
        }

        /// <summary>
        /// player will be taken normal damage
        /// </summary>
        /// <param name="damage">
        /// The damage amount
        /// </param>
        public void TakenDamage(int damage)
        {
            int trueDamage = this.shield - damage;
            if (trueDamage < 0)
            {
                this.shield = 0;
            }
            this.health += trueDamage;
        }

        /// <summary>
        /// Reset player shield to 0
        /// </summary>
        public void ResetShield()
        {
            this.shield = 0;
        }

        /// <summary>
        /// Add player shield
        /// </summary>
        /// <param name="amount">
        /// The amount of shield
        /// </param>
        public void addShield(int amount)
        {
            this.shield += amount;
        }

        public bool IsDead()
        {
            if (health <= 0)
            {
                return true;
            }
            return false;
        }
    }
}