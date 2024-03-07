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
        private void Start()
        {
            energyText = energyTextGameObject.GetComponent<Text>();
        }
        
        public void setEnergy(int energy)
        {
            energyText.text = energy.ToString();
        }

    }

}
