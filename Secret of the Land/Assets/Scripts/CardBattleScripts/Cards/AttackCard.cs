using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{
    int energyCost = 2;

    public AttackCard()
    {
        
    }

    public override void Effect()
    {
        Debug.Log("Processing the AttackCard effect");
    }
}
