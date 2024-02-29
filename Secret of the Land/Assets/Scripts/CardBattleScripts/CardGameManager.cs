using JetBrains.Annotations;
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
    public int maxHandCards { get; set; }

    // List to store your deck
    public List<int> deck = new List<int>();

    // Hand card game object
    public GameObject handCard;


    private void Awake()
    {
        drawPile = new List<int>();
        discardPile = new List<int>();
        handCards = new List<int>();
        maxHandCards = 5;
    }

    private void Start()
    {
        OnlyGetAttackCard();
    }

    // Debugging
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("startDrawing");
            DrawACard();
            UpdateHandCard();
        }
    }

    // This will be used to start player round
    private void StartPlayerRound()
    {
        DrawACard();
    }

    private void UpdateHandCard()
    {
        handCard.GetComponent<HandCardManager>().UpdateHandCard(this.handCards);
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
                Debug.Log("No card in deck");
            }


        }
        else
        {
            Debug.Log("Your hand cards are full");
        }
    }

    private Card CodeToCard(int code)
    {
        switch (code)
        {
            case 1:
                return new AttackCard();
        }
        Debug.Log("Card Code is not found in CodeToCard");
        return null;

    }

    // This will remove a card in hand cards
    public void UseACard(int code)
    {
        handCards.Remove(code);
        // UpdateHandCard();
    }

    

    // For debugging, this will let the drawPile become a stack of attack cards
    private void OnlyGetAttackCard()
    {
        this.deck = new List<int>();
        for(int i = 0; i < 10; i++)
        {
            int code = Random.Range(1, 3);
            this.deck.Add(code);
        }
    }


}


