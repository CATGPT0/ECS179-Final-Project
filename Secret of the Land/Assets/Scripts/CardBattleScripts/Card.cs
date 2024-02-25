using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, ICard
{
    int energyCost = 0;



    public virtual void Effect()
    {
        Debug.Log("Proccessing the effect of the card");
    }
}
