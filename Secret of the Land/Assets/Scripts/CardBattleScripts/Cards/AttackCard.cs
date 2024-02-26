using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : Card
{

    Transform spriteTransform;

    private void Awake()
    {
        energyCost = 2;
    }

    public AttackCard()
    {
        
    }

    public override void Effect()
    {
        Debug.Log("Processing the AttackCard effect");
    }
}
