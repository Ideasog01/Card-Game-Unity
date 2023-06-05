using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public void CastSpell(Card spellCard, SlotController target)
    {
        string function = spellCard.SpellFunction;

        switch(function)
        {
            case "ArcaneMissiles":

                ArcaneMissiles(target.AssignedCreatureController);

                break;
        }
    }

    private void ArcaneMissiles(CreatureController target)
    {
        target.DamageCreature(2);
    }
}
