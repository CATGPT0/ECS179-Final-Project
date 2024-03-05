using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
namespace CardBattle
{

    public class CardGameManager : MonoBehaviour
    {
        // The pile that you will draw card from
        public List<int> drawPile;

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

        // Record the stage of the game
        public GameStage gameStage;
        private bool finishedTheStage;




        private void Awake()
        {
            // Reset the card piles
            drawPile = new List<int>();
            discardPile = new List<int>();
            handCards = new List<int>();
            maxHandCards = 5;
        }

        private void Start()
        {
            gameStage = GameStage.beforePlayerRound;
            finishedTheStage = false;
            OnlyGetAttackCard();
        }

        // Check the stage of our game and manage them
        private void Update()
        {

            // If we finished the stage, move to next stage
            if(finishedTheStage == true)
            {
                finishedTheStage = false;
                GameStageController.MoveToNextStage(ref gameStage);
            }

            // If we haven't finished the stage, do what we should do
            // This can use delegate instead of a lot of switch. Change the delegate when entering a stage and call it in update
            if (!finishedTheStage)
            {
                switch (gameStage)
                {
                    case GameStage.beforePlayerRound:
                        BeforePlayerRoundUpdate();
                        break;
                    case GameStage.playerRound:
                        PlayerRoundUpdate();
                        break;
                    case GameStage.afterPlayerRound:
                        AfterPlayerRoundUpdate();
                        break;
                    case GameStage.beforeEnemyRound:
                        BeforeEnemyRoundUpdate();
                        break;
                    case GameStage.enemyRound:
                        EnemyRoundUpdate();
                        break;
                    case GameStage.afterEnemyRound:
                        AfterEnemyRoundUpdate();
                        break;
                }

            }


        }

        // This is for before player round
        // Following actions will happen:
        // 1. Draw card for player
        // 2. If the draw pile is empty, use discard pile to refill draw pile
        // 3. Move to next stage
        private void BeforePlayerRoundUpdate()
        {
            // 1. Draw cards for player
            if (this.drawPile.Count <= 0)
            {
                Debug.Log("Trying to transfer cards from discard pile to draw pile");
                drawPile = new List<int>(discardPile);
                discardPile.Clear();
            }

            // 2. If the draw pile is empty, use discard pile to refill draw pile

            DrawACard();
            DrawACard();

            UpdateHandCard();

            // 3. Move to next stage
            finishedTheStage = true;

        }

        private void PlayerRoundUpdate()
        {

        }

        private void AfterPlayerRoundUpdate()
        {
            finishedTheStage = true;
        }

        private void BeforeEnemyRoundUpdate()
        {
            finishedTheStage = true;
        }

        private void EnemyRoundUpdate()
        {
            finishedTheStage = true;
        }
        
        private void AfterEnemyRoundUpdate()
        {
            finishedTheStage = true;
        }

        // Check if the end round button was pressed
        // If it was pressed in the round of player, move to next round
        public void PressedEndRoundButton()
        {

            if(this.gameStage == GameStage.playerRound)
            {
                finishedTheStage = true;
            }
            
        }
        

        private void UpdateHandCard()
        {
            handCard.GetComponent<HandCardManager>().UpdateHandCard(this.handCards);
        }

        // DrawACard will randomly choose a card from drawPile and put it into your hand cards
        public void DrawACard()
        {
            // If the number of cards is not maximum
            if (handCards.Count < maxHandCards)
            {

                if (drawPile.Count > 0)
                {
                    // Get a card
                    int nextCard = drawPile[Random.Range(0, drawPile.Count)];

                    // Put it into your hand cards
                    handCards.Add(nextCard);

                    drawPile.Remove(nextCard);
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

        // This will remove a card in hand cards and throw it into discardPile
        public void UseACard(int code)
        {
            handCards.Remove(code);
            discardPile.Add(code);
            // UpdateHandCard();
        }

        // For debugging, this will let the drawPile become a stack of attack cards
        private void OnlyGetAttackCard()
        {
            this.drawPile = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                int code = Random.Range(1, 3);
                this.drawPile.Add(code);
            }
        }


    }

}


