using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using NaughtyAttributes;
using UI;
using UnityEngine;

public class ActionCardController : MonoBehaviour
{
    [SerializeField] BattleUIDisplay cardsDisplayUI;
    [SerializeField] CardDataContainer cardDataContainer;

    private void Start()
    {
        cardsDisplayUI.InitActions(OnMouseEntered, OnMouseExited, OnMouseClicked);
        GetNewCards();
    }

    private void OnMouseClicked(int index)
    {
        if (index >= cardDataContainer.CurrentCardsHolding.Count)
            return;
        cardDataContainer.CurrentCardsHolding.RemoveAt(index);
        cardsDisplayUI.PLayCard(index);
    }

    private void OnMouseExited(int index)
    {

    }

    private void OnMouseEntered(int index)
    {
        if (index >= cardDataContainer.CurrentCardsHolding.Count)
            return;
        cardsDisplayUI.UpdateCardDescription(cardDataContainer.CurrentCardsHolding[index]);
    }

    [Button]
    public void GetNewCards()
    {
        int cardNumbers = 0;
        List<CardData> cardDatasToSpawn = new();
        (cardNumbers, cardDatasToSpawn) = cardDataContainer.GetNewsCardsFromDeck();
        cardsDisplayUI.GenerateCards(cardDatasToSpawn);
    }


}
