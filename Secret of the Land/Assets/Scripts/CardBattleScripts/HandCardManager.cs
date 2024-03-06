using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;

namespace CardBattle
{


    public class HandCardManager : MonoBehaviour
    {
        // Prefebs
        public GameObject attackCard;
        public GameObject defendCard;

        // Game object for new card after Update
        private GameObject newCard;
        private List<GameObject> cards = new List<GameObject>();

        // Will take the list of hand card and update the cards
        public void UpdateHandCard(List<int> handCards)
        {
            // Every time we update the hand card, we will destroy the previous one and create a new list
            DestroyAllCards();

            if (handCards.Count > 10)
            {
                Debug.Log("Cannot update handCards due to the size of handCard is" + handCards.Count);
                return;
            }

            float xOffsetEachCard = 1f;
            int counter = 0;
            Vector3 newPosition;
            int numberOfCard = handCards.Count;

            // Because the object hand card is in the middle, we need to use TotalXOffset
            // to make sure the hand card is in the middle of screen
            float TotalXOffset = - xOffsetEachCard * (handCards.Count - 1) / 2;
            Debug.Log("TotalXOffset is " + TotalXOffset);
            foreach (int i in handCards)
            {
                GameObject cardPrefeb = codeToCardObject(i);
                newPosition = this.transform.position + new Vector3(xOffsetEachCard * counter + TotalXOffset, 0f, 0f);
                newCard = Instantiate(cardPrefeb, newPosition, Quaternion.identity, this.transform);
                cards.Add(newCard);
                newCard.GetComponent<Card>().sprite.GetComponent<SpriteRenderer>().sortingOrder = counter;

                ++counter;
            }
        }

        private GameObject codeToCardObject(int code)
        {
            switch (code)
            {
                case 1:
                    return attackCard;
                case 2:
                    return defendCard;
                default:
                    Debug.Log("No card prefeb found with code" + code);
                    return null;
            }
        }

        private void DestroyAllCards()
        {
            foreach (GameObject card in this.cards)
            {
                Destroy(card);
            }

            cards.Clear();
        }

    }
}

