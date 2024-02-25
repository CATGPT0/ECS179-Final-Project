using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    // The pile that you will draw card from
    List<int> drawPile { get; set; }

    // The pile that you will discard to
    List<int> discardPile { get; set; }

    // The list for your hand cards
    List<int> handCards { get; set; }
    // The maximum hand cards you can hold
    int maxHandCards { get; set; }

    // List to store your deck
    List<int> deck = new List<int>();


    private void Awake()
    {
        drawPile = new List<int>();
        discardPile = new List<int>();
        handCards = new List<int>();
        maxHandCards = 1;
    }
    // DrawACard will randomly choose a card from drawPile and put it into your hand cards
    public void DrawACard()
    {
        // If the number of cards is not maximum
        if(handCards.Count < maxHandCards)
        {
            if(deck.Count > 0)
            {
                // Get a card
                int nextCard = deck[Random.Range(0, deck.Count)];

                // Put it into your hand cards
                handCards.Add(nextCard);

                deck.Remove(nextCard);
            }
            else
            {
                Debug.Log("No card in DrawPile");
            }


        }
        else
        {
            Debug.Log("Your hand cards are full");
        }
    }

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


