using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CardBattle;
using TMPro;

namespace CardBattle
{


    public class PlayerUIManager : MonoBehaviour
    {
        public GameObject energyTextGameObject;
        private Text energyText;
        public GameObject healthTextGameObject;
        private Text healthText;
        public GameObject shieldTextGameObject;
        private Text shieldText;
        public GameObject enemyActionTextGameObject;
        private Text enemyActionText;
        public GameObject enemyHealthTextGameObject;
        private Text enemyHealthText;
        private void Awake()
        {
            energyText = energyTextGameObject.GetComponent<Text>();
            healthText = healthTextGameObject.GetComponent<Text>();
            shieldText = shieldTextGameObject.GetComponent<Text>();
            enemyActionText = enemyActionTextGameObject.GetComponent<Text>();
            enemyHealthText = enemyHealthTextGameObject.GetComponent<Text>();

        }
        
        public void setEnergy(int energy)
        {
            energyText.text = energy.ToString();
            
        }


        public void setHealth(int health)
        {
            healthText.text = health.ToString();
        }

        public void setShield(int amount)
        {
            shieldText.text = amount.ToString();
        }

        public void setEnemyAction(string actionString)
        {
            enemyActionText.text = actionString;
        }

        public void setEnemyHealth(int amount)
        {
            enemyHealthText.text = amount.ToString();
        }

    }

}
