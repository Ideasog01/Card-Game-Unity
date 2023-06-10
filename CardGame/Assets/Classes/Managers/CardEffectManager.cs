using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardEffectManager : MonoBehaviour
{
    public void OnStartTurnEffect(Card card)
    {
        if(card.name == "House")
        {
            GameUtilities.AddMana(GameplayManager.activePlayer, 2, 1);
        }
    }

    public void OnEndTurnEffect(Card card)
    {

    }

    public void OnCardReleaseEffect(Card card, Target target)
    {
        if(card.name == "Arcane Missiles")
        {
            target.GetComponent<EntityController>().TakeDamage(2);
        }
    }
}
