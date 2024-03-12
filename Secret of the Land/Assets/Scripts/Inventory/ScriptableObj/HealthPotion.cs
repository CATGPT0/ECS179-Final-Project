using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealthPotion", menuName = "Inventory/Consumable/Potion/HealthPotion")]
public class HealthPotion : Potion
{
    
    void OnEnable()
    {
        //onItemUse += PlayerController.Instance.Player.ChangeHealth;
    }
}
