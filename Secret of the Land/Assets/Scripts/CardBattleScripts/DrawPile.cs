using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using CardBattle;


namespace CardBattle
{
    public class DrawPile : MonoBehaviour
    {
        public CardGameManager cardGameManager;

        public GameObject sprite;

        private Transform drawPileTransform;

        private Transform spriteTransform;

        private Vector3 spriteOriginPosition;

        private void Awake()
        {
            drawPileTransform = GetComponent<Transform>();

            spriteTransform = sprite.GetComponent<Transform>();

            spriteOriginPosition = spriteTransform.position;
        }

        private void OnMouseDown()
        {
            cardGameManager.DrawACard();
        }

    }
}
