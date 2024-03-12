using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EquipControler : MonoBehaviour
{
    public GameItem weapon;
    public Image image;
    public Sprite originalImage;

    void Start()
    {
        originalImage = image.sprite;
    }

    public void EquipUp(GameItem inputItem)
    {
        if (weapon != null)
        {
            TearDown();
        }
        inputItem.onItemEquip.Invoke(inputItem.item.value);
        image.sprite = inputItem.item.itemImage;
        weapon = inputItem;
    }

    public void TearDown()
    {
        weapon.onItemTeardown.Invoke(-weapon.item.value);
        weapon = null;
        image.sprite = originalImage;
    }
}
