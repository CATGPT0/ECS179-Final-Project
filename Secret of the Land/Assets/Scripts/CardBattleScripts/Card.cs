using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;
using Schema;
using Unity.VisualScripting;
// using static SchemaEditor.Internal.CopyBuffer;

namespace CardBattle
{

    public class Card : MonoBehaviour, ICard
    {
        protected int energyCost = 0;

        public GameObject sprite;

        public HandCardManager handCard;

        public CardGameManager gameManager;

        public CardBattleManager cardBattleManager;

        protected Transform spriteTransform;

        protected Vector3 spriteOriginPosition;

        public int cardCode;

        protected int originLayerOrder;

        protected Enemy enemy;

        public CardScriptableObject cardScriptableObject;

        public new string name;

        public string description;

        private Transform gameObjectTransform;

        private Vector3 gameObjectOriginPosition;

        private Vector3 drawPilePosition;

        private bool processedEffect = false;

        private bool stayOnCard = false;

        private float timeOffsetOfDescription = 1f;

        private float timerForDescription = 0;

        // For using card animation
        private bool usingTheCard = false;

        private float timeCounter = 0;
        private float duration = 0.3f;
        private Vector3 velocity = Vector3.zero;


        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<CardGameManager>();
            handCard = GameObject.Find("HandCard").GetComponent<HandCardManager>();
            enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            cardBattleManager = GameObject.Find("CardBattleManager").GetComponent<CardBattleManager>();
            spriteTransform = sprite.GetComponent<Transform>();
            spriteOriginPosition = spriteTransform.position;
            gameObjectTransform = this.gameObject.GetComponent<Transform>();
            gameObjectOriginPosition = gameObjectTransform.position;



        }

        private void Start()
        {
            energyCost = cardScriptableObject.energyCost;

            cardCode = cardScriptableObject.cardCode;
            name = cardScriptableObject.cardName;
            drawPilePosition = gameManager.drawPileGameObject.GetComponent<Transform>().position;
            description = cardScriptableObject.description;
            // Debug.Log(spriteOriginPosition);


        }

        private void Update()
        {
            // Debug.Log(timerForDescription);
            if (stayOnCard)
            {
                timerForDescription += Time.deltaTime;
                if(timerForDescription >= timeOffsetOfDescription)
                {
                    gameManager.EnableDescription(description);
                    // gameManager.SetDescription(description);
                }
            }
            if (usingTheCard)
            {
                if (!processedEffect)
                {
                    Debug.Log("Using " + cardCode);
                    gameManager.UseACard(cardCode, energyCost);
                    processedEffect = true;
                }
                
                timeCounter += Time.deltaTime;
                gameObjectTransform.position = Vector3.SmoothDamp(gameObjectTransform.position, drawPilePosition, ref velocity, duration);
                gameObjectTransform.localScale = Vector3.Lerp(gameObjectTransform.localScale, Vector3.zero, timeCounter/(duration*150));
                // Debug.Log(gameObjectTransform.localScale);
                if(timeCounter >= duration + 1)
                {
                   
                    Destroy(this.gameObject);
                }
            }
            
        }


        private void OnMouseEnter()
        {
            // gameManager.EnableDescription();
            if (this.gameManager.gameStage != GameStage.playerRound)
            {
                return;
            }
            spriteOriginPosition = spriteTransform.position;
            spriteTransform.position = spriteTransform.position + new Vector3(0f, 0.2f, 0f);
            originLayerOrder = this.sprite.GetComponent<SpriteRenderer>().sortingOrder;
            this.sprite.GetComponent<SpriteRenderer>().sortingOrder += 40;
            this.stayOnCard = true;
            
        }

        private void OnMouseExit()
        {
            gameManager.DisableDescription();
            this.timerForDescription = 0;
            if (this.gameManager.gameStage != GameStage.playerRound)
            {
                return;
            }
            spriteTransform.position = spriteOriginPosition;
            this.sprite.GetComponent<SpriteRenderer>().sortingOrder = originLayerOrder;
            this.stayOnCard = false;
        }

        private void OnMouseDown()
        {

            if(gameManager.CanUseACard(cardCode, energyCost))
            {
                usingTheCard = true;
                this.GetComponent<Collider2D>().enabled = false;
                // Debug.Log("can use the card");
            }

            //if (gameManager.UseACard(cardCode, energyCost))
            //{
            //    Destroy(this.gameObject);
            //}
            
        }

        public virtual void Effect()
        {
            Debug.Log("Proccessing the effect of the card");
        }

        public void ProcessEffect()
        {
            this.cardBattleManager.ProcessCardEffect(this.cardCode);
        }

        public void setColor(Color color)
        {
            this.sprite.GetComponent<SpriteRenderer>().color = color;
        }


    }
}
