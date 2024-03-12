using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using UnityEngine;

public class GameItem
{
    public Item item;
    public Action<int> onItemUse;

    public GameItem(Item item)
    {
        this.item = item;
        onItemUse += PlayerController.Instance.Player.ChangeHealth;
    }
}