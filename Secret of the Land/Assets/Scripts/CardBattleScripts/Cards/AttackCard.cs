using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackCard : Card
{

    private void Start()
    {
        energyCost = 2;
        cardCode = 1;
    }

    public override void Effect()
    {
        Debug.Log("Processing the AttackCard effect");
    }

}
