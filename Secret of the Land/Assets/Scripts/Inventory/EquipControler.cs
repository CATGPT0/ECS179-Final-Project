using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EquipControler : MonoBehaviour
{
    private Weapon weapon;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void EquipUp()
    {
        //weapon.onItemEquip?.Invoke(weapon.value);
        image.sprite = weapon.itemImage;
    }

    public void TearDown()
    {
        //weapon.onItemTearDown.Invoke(-weapon.value);
    }
}
