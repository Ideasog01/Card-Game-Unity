using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public virtual void CastSpell(Card spellCard, SlotController target)
    {
        string function = spellCard.SpellFunction;

        switch(function)
        {
            case "ArcaneMissiles":

                target.AssignedCreatureController.DamageCreature(2);

                break;
        }
    }

    public virtual void CastSpell(Card spellCard, EntityController target)
    {
        string function = spellCard.SpellFunction;

        switch (function)
        {
            case "ArcaneMissiles":

                target.TakeDamage(2);
                Debug.Log("Arcane Missiles Cast!");

                break;
        }
    }
}
