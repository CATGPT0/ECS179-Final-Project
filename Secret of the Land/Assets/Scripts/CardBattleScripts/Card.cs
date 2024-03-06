using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;

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

        protected Vector3 originPosition;

        protected int cardCode;

        protected int originLayerOrder;

        protected Enemy enemy;



        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<CardGameManager>();
            handCard = GameObject.Find("HandCard").GetComponent<HandCardManager>();
            enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            cardBattleManager = GameObject.Find("CardBattleManager").GetComponent<CardBattleManager>();
            spriteTransform = sprite.GetComponent<Transform>();
            originPosition = spriteTransform.position;

        }



        private void OnMouseEnter()
        {
            spriteTransform.position = spriteTransform.position + new Vector3(0f, 0.2f, 0f);
            originLayerOrder = this.sprite.GetComponent<SpriteRenderer>().sortingOrder;
            this.sprite.GetComponent<SpriteRenderer>().sortingOrder += 40;
        }

        private void OnMouseExit()
        {
            spriteTransform.position = originPosition;
            this.sprite.GetComponent<SpriteRenderer>().sortingOrder = originLayerOrder;
        }

        private void OnMouseDown()
        {

            if (gameManager.UseACard(cardCode, energyCost))
            {
                Destroy(this.gameObject);
            }
            
        }

        public virtual void Effect()
        {
            Debug.Log("Proccessing the effect of the card");
        }

        public void ProcessEffect()
        {
            this.cardBattleManager.ProcessCardEffect(this.cardCode);
        }


    }
}
