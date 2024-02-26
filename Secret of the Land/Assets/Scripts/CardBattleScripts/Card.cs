using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, ICard
{
    protected int energyCost = 0;

    public GameObject sprite;

    public GameObject handCard;

    public GameObject GameManager;

    private void Awake()
    {
        // Find the gameManager to get access of the game
        GameManager = GameObject.Find("GameManger");
    }
    public virtual void Effect()
    {
        Debug.Log("Proccessing the effect of the card");
    }


}
