using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    public virtual void CardEffect(Card card, SlotController target)
    {
        string function = card.CardEffectFunction;

        switch(function)
        {
            case "ArcaneMissiles":

                target.AssignedCreatureController.DamageCreature(2);

                break;
        }
    }

    public virtual void CardEffect(Card card, EntityController target)
    {
        string function = card.CardEffectFunction;

        switch (function)
        {
            case "ArcaneMissiles":

                target.TakeDamage(2);
                Debug.Log("Arcane Missiles Cast!");

                break;
        }
    }

    public virtual void CardEffect(EntityController owner, Card card)
    {
        string function = card.CardEffectFunction;

        switch(function)
        {
            case "House":

                owner.ManaAmountArray[2] += 1;
                GameplayManager.cardDisplayManager.DisplayMana();
                Debug.Log("House Effect");
                break;
        }
    }
}
