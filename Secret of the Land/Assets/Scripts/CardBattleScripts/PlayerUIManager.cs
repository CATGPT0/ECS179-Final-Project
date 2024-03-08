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
        private void Awake()
        {
            energyText = energyTextGameObject.GetComponent<Text>();
            healthText = healthTextGameObject.GetComponent<Text>();
        }
        
        public void setEnergy(int energy)
        {
            energyText.text = energy.ToString();
            
        }


        public void setHealth(int health)
        {
            healthText.text = health.ToString();
        }

    }

}
