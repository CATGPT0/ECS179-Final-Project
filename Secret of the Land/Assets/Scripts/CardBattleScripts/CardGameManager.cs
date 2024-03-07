using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
namespace CardBattle
{

    public class CardGameManager : MonoBehaviour
    {
        // The input of Card Game Manger should include:
        // Player HP
        // Deck


        // The pile that you will draw card from
        public List<int> drawPile;

        // The pile that you will discard to
        List<int> discardPile { get; set; }

        // The list for your hand cards
        List<int> handCards { get; set; }
        // The maximum hand cards you can hold
        public int maxHandCards;

        // List to store your deck
        public List<int> deck = new List<int>();

        // Hand card game object
        public GameObject handCardGameObject;
        public HandCardManager handCardManager;

        // Record the stage of the game
        public GameStage gameStage;
        private bool finishedTheStage;

        // Game Battle manager
        public GameObject cardBattleManagerGameObject;
        private CardBattleManager cardBattleManager;

        // If the "End Your Turn buttom was pressed
        private bool OnClickEndYourTurn;

        // Enemy
        public GameObject enemyGameObject;
        public Enemy enemy;

        // Player
        public GameObject playerGameObject;
        public Player player;

        // Bool for checking if enemy is dead
        private bool enemyIsDead = false;

        // number of cards player used
        private int cardsUsedNum;

        // Card UI manager
        public GameObject UIManagerGameObject;
        private PlayerUIManager playerUIManager;






        private void Awake()
        {
            // Reset the card piles
            drawPile = new List<int>();
            discardPile = new List<int>();
            handCards = new List<int>();
            maxHandCards = 10;
            cardsUsedNum = 0;
        }

        private void Start()
        {
            gameStage = GameStage.beforePlayerRound;
            finishedTheStage = false;
            OnlyGetAttackCard();

            OnClickEndYourTurn = false;

            handCardManager = handCardGameObject.GetComponent<HandCardManager>();
            cardBattleManager = cardBattleManagerGameObject.GetComponent<CardBattleManager>();
            enemy = enemyGameObject.GetComponent<Enemy>();
            player = playerGameObject.GetComponent<Player>();
            playerUIManager = UIManagerGameObject.GetComponent<PlayerUIManager>();

            // Reset the Action when start the game
            cardBattleManager.LoadNextEnemyAction(enemy);
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
        // 3. If the player didn't use any card last round, get some energy
        // 4. Move to next stage
        /// <summary>
        /// The method for preparing player round
        /// </summary>
        private void BeforePlayerRoundUpdate()
        {
            // 1. Draw cards for player
            if (this.drawPile.Count <= 0)
            {
                // Debug.Log("Trying to transfer cards from discard pile to draw pile");
                drawPile = new List<int>(discardPile);
                discardPile.Clear();
            }

            // 2. If the draw pile is empty, use discard pile to refill draw pile

            DrawACard();
            DrawACard();

            UpdateHandCard();

            // 3. If the player didn't use any card last round, get some energy
            if(cardsUsedNum == 0)
            {
                player.IncreaseEnergy(2);
            }

            cardsUsedNum = 0;

            // 4. Reset shield
            this.player.ResetShield();

            // 5. Update UI
            playerUIManager.setEnergy(player.energy);
            playerUIManager.setHealth(player.health);

            

            // 6. Move to next stage
            finishedTheStage = true;

            

        }

        // This is for player round
        // Following actions will happen:
        // 1. If the "End Your Turn Was Pressed", move the next stage
        private void PlayerRoundUpdate()
        {
            // 1. If the "End Your Turn Was Pressed", move the next stage
            if (OnClickEndYourTurn)
            {
                finishedTheStage = true;
                OnClickEndYourTurn = false;
            }
            
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
            cardBattleManager.DoEnemyAction();
            finishedTheStage = true;
        }
        
        private void AfterEnemyRoundUpdate()
        {
            cardBattleManager.LoadNextEnemyAction(enemy);
            enemy.ResetDamage();
            finishedTheStage = true;
        }

        // Check if the end round button was pressed
        // If it was pressed in the round of player, move to next round
        public void PressedEndRoundButton()
        {

            if(this.gameStage == GameStage.playerRound)
            {
                OnClickEndYourTurn = true;
            }
            
        }
        

        private void UpdateHandCard()
        {
            handCardManager.UpdateHandCard(this.handCards);
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

        /// <summary>
        /// This will remove a card in hand cards and throw it into discardPile.
        /// </summary>
        /// <param name="code">
        /// The code of the card that will be used
        /// </param>
        /// <returns>
        /// True if the card is used, false if the card is fail to use
        /// </returns>
        public bool UseACard(int code, int energyCost)
        {
            
            // If the energy is not enough, tell the card that should not destroy itself
            if(!player.ReduceEnergy(energyCost))
            {
                return false;
            }

            // Accumulate the cards used
            ++cardsUsedNum;

            // Reset energy
            playerUIManager.setEnergy(player.energy);

            // Remove the card from hand
            handCards.Remove(code);

            // process the card effect
            cardBattleManager.ProcessCardEffect(code);
            
            // Put the used card in discard pile
            discardPile.Add(code);

            // If the enemy is dead, do something else
            if (enemy.HP <= 0)
            {
                enemy.Die();
                enemyIsDead = true;
            }

            // return 
            return true;

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


