using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendCard : Card
{
    private void Start()
    {
        energyCost = 1;
        cardCode = 2;
    }

    public override void Effect()
    {
        Debug.Log("Processing DefendCard effect");
    }
}
