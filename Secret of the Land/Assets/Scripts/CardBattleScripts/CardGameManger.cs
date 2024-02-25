using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManger : MonoBehaviour
{
    // The pile that you will draw card from
    List<int> drawPile = new List<int>();

    // The pile that you will discard to
    List<int> discardPile = new List<int>();

    // The list for your hand cards
    List<int> handCards = new List<int>();

    // List to store your deck
    List<int> deck = new List<int>();
   


    private Card codeToCard(int code)
    {
        switch (code)
        {
            case 1:
                return new AttackCard();
            default:
                Debug.Log("Card Code is not found");
                return null;
        }
        
    }

    // For debugging, this will let the drawPile become a stack of attack cards
    private void OnlyGetAttackCard()
    {
        this.drawPile = new List<int>();
        for(int i = 0; i < 10; i++)
        {
            this.drawPile.Add(1);
        }
    }


}


