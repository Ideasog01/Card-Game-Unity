using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

public static class GameUtilities
{
    public static void DrawCard(EntityController entity)
    {
        if (entity.PlayerCards.Count == 0)
        {
            //Player Take Damage
            entity.PlayerHealth -= entity.FatigueAmount;
            entity.FatigueAmount++;
            return;
        }

        Card newCard = entity.PlayerCards[entity.PlayerCards.Count - 1];

        if (entity.PlayerHand.Count < 10)
        {
            entity.PlayerHand.Add(newCard);
        }
        else
        {
            //Play Card Destroyed Animation
        }

        entity.PlayerCards.RemoveAt(entity.PlayerCards.Count - 1);
    }

    public static void RemoveCard(EntityController entity, Card card)
    {
        entity.PlayerHand.Remove(card);
    }

    public static void ShuffleHand(EntityController entity)
    {
        List<Card> playerCards = entity.PlayerCards;

        for (int i = 0; i < playerCards.Count; i++)
        {
            int rnd = Random.Range(0, playerCards.Count);
            Card tempCard = playerCards[rnd];
            playerCards[rnd] = playerCards[i];
            playerCards[i] = tempCard;
        }

        entity.PlayerCards = playerCards;
    }

    public static void AddMana(EntityController entity, int manaType, int amount)
    {
        entity.ManaAmountArray[manaType] += amount;
    }

    public static bool HasMana(EntityController entity, int amount, int manaIndex)
    {
        int manaAmount = entity.ManaAmountArray[manaIndex]; //The mana the player has

        //Amount is the number of mana to check/remove

        if (manaAmount >= amount)
        {
            manaAmount -= amount;
            return true;
        }

        return false;
    }
}
