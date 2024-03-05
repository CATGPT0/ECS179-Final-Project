using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardBattle;

namespace CardBattle
{


    public class HandCardManager : MonoBehaviour
    {

        public GameObject attackCard;

        public GameObject defendCard;

        private List<GameObject> cards = new List<GameObject>();

        // Will take the list of hand card and update the cards
        public void UpdateHandCard(List<int> handCards)
        {
            // Every time we update the hand card, we will destroy the previous one and create a new list
            DestroyAllCards();

            if (handCards.Count > 5)
            {
                Debug.Log("Cannot update handCards due to the size of handCard is" + handCards.Count);
                return;
            }

            Vector3 xOffset = new Vector3(2, 0, 0);
            int counter = 0;

            foreach (int i in handCards)
            {
                GameObject cardPrefeb = codeToCardObject(i);

                cards.Add(Instantiate(cardPrefeb, this.transform.position + xOffset * counter, Quaternion.identity, this.transform));

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

