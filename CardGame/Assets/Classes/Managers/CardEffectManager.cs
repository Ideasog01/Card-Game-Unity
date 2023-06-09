using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    public void CardEffect(Card card, EntityController target)
    {
        string function = card.CardEffectFunction;

        switch (function)
        {
            case "ArcaneMissiles":

                target.TakeDamage(2);
                Debug.Log("Arcane Missiles Cast!");

                break;

            case "House":

                target.ManaAmountArray[2] += 1;
                GameplayManager.cardDisplayManager.DisplayMana();
                Debug.Log("House Effect");

                break;
        }
    }
}
