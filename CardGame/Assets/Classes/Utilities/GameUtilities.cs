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
        int manaTotal = 0;

        foreach (int manaCount in entity.ManaAmountArray)
        {
            manaTotal += manaCount;
        }

        if (manaTotal < 12) //12 is the max mana
        {
            entity.ManaAmountArray[manaType] += amount;
        }
    }

    public static void ResetMana(EntityController entity)
    {
        for(int i = 0; i < entity.ManaAmountArray.Length; i++)
        {
            entity.ManaAmountArray[i] = 0;
        }
    }

    public static bool HasMana(EntityController entity, int amount, int manaIndex)
    {
        int manaAmount = entity.ManaAmountArray[manaIndex]; //The mana the player has

        //Amount is the number of mana to check/remove

        if (manaAmount >= amount)
        {
            entity.ManaAmountArray[manaIndex] -= amount;
            return true;
        }

        return false;
    }

    public static bool IsCreatureRange(CreatureController potentialTarget, CreatureController attacker)
    {
        Card.Range range = attacker.CreatureCard.CreatureReach;

        if (range == Range.InfiniteReach)
        {
            return true;
        }

        float distance = Vector2.Distance(attacker.transform.position, potentialTarget.transform.position);

        if(range == Range.NormalReach)
        {
            return distance < 1.6f;
        }

        if(range == Range.FarReach)
        {
            return distance < 3.2f;
        }

        return false;
    }
}
