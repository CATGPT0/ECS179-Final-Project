using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using Unity.VisualScripting;
using Manager;

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
        public List<int> handCards;
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

        // Bool for checking if the player choose to draw card or get energy
        // 1 when player get energy, 2 when player draw card, 0 when player have not decided yet
        private int playerChoice = 0;

        public GameObject drawPileGameObject;

        // description for cards
        public GameObject descriptionGameObject;
        private DescriptionController descriptionController;

        // Result counter
        float resultCounter = 0;


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
            descriptionController = descriptionGameObject.GetComponent<DescriptionController>();

            // Reset the Action when start the game
            cardBattleManager.LoadNextEnemyAction(enemy);

            DrawACard();
            DrawACard();
            DrawACard();
            UpdateHandCard();

            // UI setting
            
            playerUIManager.setEnergy(player.energy);
            playerUIManager.setHealth(player.health);
            playerUIManager.setShield(player.shield);
            playerUIManager.setEnemyAction(cardBattleManager.enemyNextActionInformation);
            playerUIManager.setEnemyHealth(enemy.HP);
            playerUIManager.setEnemyShield(enemy.shield);
            

            
        }

        // Check the stage of our game and manage them
        private void Update()
        {
            if (enemyIsDead)
            {
                resultCounter += Time.deltaTime;
                Debug.Log(resultCounter);
                if(resultCounter > 3f)
                {
                    Debug.Log("Win!");
                    SceneManager.LoadWorldScene();
                }
                return;
            }
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
            // handCardManager.SetCardUnavaliableUpdate();
            handCardManager.SetCardColorUnavaliable();
            // 1. If the draw pile is empty, use discard pile to refill draw pile
            if (this.drawPile.Count <= 0)
            {
                // Debug.Log("Trying to transfer cards from discard pile to draw pile");
                drawPile = new List<int>(discardPile);
                discardPile.Clear();
            }

            switch (playerChoice)
            {
                case 1:
                    player.IncreaseEnergy(2);
                    break;
                case 2:
                    // If player's cards are max
                    if(handCards.Count >= 10)
                    {
                        playerChoice = 0;
                        break;
                    }
                    else
                    {
                        DrawACard();
                        DrawACard();
                    }
                    break;
                default:
                    break;
            }

            // DrawACard();
            // DrawACard();

            

            

            if (playerChoice != 0)
            {
                // Reset card used
                cardsUsedNum = 0;

                

                // 5. Update UI
                playerUIManager.setEnergy(player.energy);
                playerUIManager.setHealth(player.health);

                // Update Hand Card
                UpdateHandCard();
                playerChoice = 0;
                // 6. Move to next stage
                finishedTheStage = true;
            }

            

        }

        // This is for player round
        // Following actions will happen:
        // 1. If the "End Your Turn Was Pressed", move the next stage
        private void PlayerRoundUpdate()
        {
            // 1. If the "End Your Turn Was Pressed", move the next stage
            // handCardManager.SetCardAvaliableUpdate();
            if (OnClickEndYourTurn)
            {
                finishedTheStage = true;
                OnClickEndYourTurn = false;
            }
            
        }

        private void AfterPlayerRoundUpdate()
        {
            UpdateHandCard();
            // handCardManager.SetCardUnavaliableUpdate();
            finishedTheStage = true;
        }

        private void BeforeEnemyRoundUpdate()
        {
            finishedTheStage = true;
        }

        private void EnemyRoundUpdate()
        {
            cardBattleManager.DoEnemyAction();
            
            playerUIManager.setHealth(player.health);

            finishedTheStage = true;
        }
        
        private void AfterEnemyRoundUpdate()
        {
            enemy.ResetDamage();
            cardBattleManager.LoadNextEnemyAction(enemy);
            
            playerUIManager.setEnemyAction(cardBattleManager.enemyNextActionInformation);
            // Reset player shield
            this.player.ResetShield();
            playerUIManager.setShield(player.shield);
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
        
        public void PressedEnergyButton()
        {
            if(this.gameStage == GameStage.beforePlayerRound)
            {
                playerChoice = 1;
            }
        }

        public void PressedDrawPileButton()
        {
            if(this.gameStage == GameStage.beforePlayerRound)
            {
                // Debug.Log("Draw Pile was pressed");
                playerChoice = 2;
            }
        }

        private void UpdateHandCard()
        {
            handCardManager.UpdateHandCard(this.handCards);
        }

        // DrawACard will randomly choose a card from drawPile and put it into your hand cards
        public void DrawACard()
        {
            ResetDrawPile();
            // Debug.Log("Draw card");
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

            // If it is not player stage, stop using card
            if (this.gameStage != GameStage.playerRound)
            {
                return false;
            }

            // If the energy is not enough, tell the card that should not destroy itself
            if (!player.ReduceEnergy(energyCost))
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

            // Update enemy UI
            playerUIManager.setEnemyHealth(enemy.HP);
            playerUIManager.setEnemyShield(enemy.shield);

            // Update player Ui
            playerUIManager.setShield(player.shield);

            // If the enemy is dead, tell world we won
            if (enemy.HP <= 0)
            {
                //enemy.Die();
                enemyIsDead = true;
                this.GetComponent<EnemySetter>().GameStateSetter(true);
                
                


            }

            // return 
            return true;

        }


        public bool CanUseACard(int code, int energyCost)
        {
            if (this.gameStage != GameStage.playerRound)
            {
                return false;
            }

            // If the energy is not enough, tell the card that should not destroy itself
            if (player.energy < energyCost)
            {
                return false;
            }

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

        private void ResetDrawPile()
        {
            if (this.drawPile.Count <= 0)
            {
                drawPile = new List<int>(discardPile);
                discardPile.Clear();
            }
        }

        public void SetDescription(string description)
        {
            descriptionController.SetDescription(description);
        }

        public void EnableDescription()
        {
            Debug.Log("EnableNow in game manager");
            descriptionGameObject.SetActive(true);

        }
        public void EnableDescription(string description)
        {
            Debug.Log("EnableNow in game manager");
            descriptionGameObject.SetActive(true);
            descriptionController.SetDescription(description);

        }

        public void DisableDescription()
        {
            descriptionGameObject.SetActive(false);
        }


    }

}


