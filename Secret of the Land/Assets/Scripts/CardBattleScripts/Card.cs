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

        protected Transform spriteTransform;

        protected Vector3 originPosition;

        protected int cardCode;

        protected Enemy enemy;

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<CardGameManager>();
            handCard = GameObject.Find("HandCard").GetComponent<HandCardManager>();
            enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
            spriteTransform = sprite.GetComponent<Transform>();
            originPosition = spriteTransform.position;
        }

        private void OnMouseEnter()
        {
            spriteTransform.position = spriteTransform.position + new Vector3(0f, 0.1f, 0f);
        }

        private void OnMouseExit()
        {
            spriteTransform.position = originPosition;
        }

        private void OnMouseDown()
        {
            Effect();
            gameManager.UseACard(cardCode);
            Destroy(this.gameObject);
        }

        public virtual void Effect()
        {
            Debug.Log("Proccessing the effect of the card");
        }


    }
}
