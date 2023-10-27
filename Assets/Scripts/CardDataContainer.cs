using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class CardDataContainer : MonoBehaviour
    {
        [field: SerializeField]
        public int MaxHoldingSize { get; set; }
        public List<CardData> CurrentCardsHolding { get; set; } = new();

        // for testing first
        [field: SerializeField]
        public List<CardData> CardsDeck { get; set; } = new();

        public (int, List<CardData>) GetNewsCardsFromDeck()
        {
            int cardsNeeded = MaxHoldingSize - CurrentCardsHolding.Count;
            if (cardsNeeded <= 0)
            {
                return (0, null);
            }

            if (cardsNeeded <= CardsDeck.Count)
            {
                CardsDeck.Shuffle();
                List<CardData> cardDatas = new();
                for (int i = 0; i < cardsNeeded; i++)
                {
                    cardDatas.Add(CardsDeck[i]);
                }

                CurrentCardsHolding.AddRange(cardDatas);
                return (cardsNeeded, cardDatas);
            }
            else
            {
                Debug.Log("Not enough cards to get");
                return (0, null);
            }

        }

    }
}
