using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace UI
{
    public class BattleUIDisplay : MonoBehaviour
    {
        [SerializeField] CardDescriptionUI cardDescription;
        [SerializeField] CardDisplay cardDisplayPrefab;
        [SerializeField] CardMoverUI cardMove;
        [SerializeField] Transform cardContainer;
        [SerializeField] DetectCardActionArea detectPlayCardArea;
        List<CardDisplay> cards = new();
        private Action<int> onMouseEnterCb, onMouseExitCb, onPlayCardCb;

        public void InitActions(Action<int> mouseEnter, Action<int> mouseExit, Action<int> playCard)
        {
            onMouseEnterCb = mouseEnter;
            onMouseExitCb = mouseExit;
            onPlayCardCb = playCard;
        }

        public void GenerateCards(List<CardData> cardDatas)
        {
            foreach (var cardData in cardDatas)
            {
                var newCardDisplay = Instantiate(cardDisplayPrefab, cardContainer);
                newCardDisplay.UpdateCardInfo(cardData.CardIcon, cardData.CardName, cardData.CardDescription);
                newCardDisplay.InitAction(OnMouseEntered, OnMouseExited, OnMouseClicked, OnmouseHolded, OnMouseReleased);
                cards.Add(newCardDisplay);
            }
        }

        private void OnMouseReleased(CardDisplay cardDisplay)
        {
            int index = GetCardItemIndex(cardDisplay);
            if (index < 0)
                return;

            if (detectPlayCardArea == null || !detectPlayCardArea.IsPlayCard)
            {
                cardDisplay.CancelCard();
                cardMove.OnStopMove();
            }
            else if (detectPlayCardArea.IsPlayCard)
            {
                onPlayCardCb?.Invoke(index);
            }

        }

        private void OnmouseHolded(CardDisplay cardDisplay)
        {
            cardDisplay.OnSelectCard();
            cardMove.OnStartMove(cardDisplay.CardIcon.sprite, cardDisplay.CardName.text, "cardDisplay.CardDescription.text");
        }

        public void PLayCard(int cardIndex)
        {
            if (cardIndex < 0)
                return;
            CardDisplay cardDisplay = cards[cardIndex];
            cardDisplay.PLayCard();
            cardMove.PlayCardAnimation();
        }

        public void UpdateCardDescription(CardData cardData)
        {
            cardDescription.UpdateCardInfo(cardData.CardIcon, cardData.CardName, cardData.CardDescription);
        }


        public void OnPlayCard(CardDisplay cardDisplay)
        {
            int index = GetCardItemIndex(cardDisplay);
            if (index < 0)
                return;
            
        }

        public void OnMouseEntered(CardDisplay cardDisplay)
        {
            int index = GetCardItemIndex(cardDisplay);
            if (index < 0)
                return;
            onMouseEnterCb?.Invoke(index);
        }

        public void OnMouseExited(CardDisplay cardDisplay)
        {
            int index = GetCardItemIndex(cardDisplay);
            if (index < 0)
                return;
            onMouseExitCb?.Invoke(index);
        }

        public void OnMouseClicked(CardDisplay cardDisplay)
        {
            int index = GetCardItemIndex(cardDisplay);
            if (index < 0)
                return;
            onPlayCardCb?.Invoke(index);
        }

        private int GetCardItemIndex(CardDisplay cardDisplay)
        {
            return cards.IndexOf(cardDisplay);
        }
    }
}
