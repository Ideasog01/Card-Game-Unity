using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelteringThrone : PlayerHero
{
    [SerializeField]
    private PlayerEntityController playerEntity;

    private List<EntityController> attackers = new List<EntityController>();

    private void Awake()
    {
        GameplayManager.onTakeDamage += DivineRetribution;
        GameplayManager.onEndTurn += ClearAttackers;
        GameplayManager.onTributeActivation += DivineLoyalty;

        PlayerHeroAwake();
    }

    public void DivineRetribution() //Deal damage to attacker (Only once)
    {
        if(GameplayManager.lastEntityAttacker != null)
        {
            if (!attackers.Contains(GameplayManager.lastEntityAttacker))
            {
                GameplayManager.lastEntityAttacker.TakeDamage(1, null);
                attackers.Add(GameplayManager.lastEntityAttacker);
            }
        }
    }

    public void DivineLoyalty()
    {
        foreach(CreatureController creature in GameplayManager.creatureList)
        {
            if(creature.AssignedPlayer == playerEntity)
            {
                creature.AddEffect(GameplayManager.cardEffectManager.divineShield);
                Debug.Log("Divine shield added to creature: " + creature.gameObject.name);
            }
        }

        GameplayManager.onTributeActivation -= DivineLoyalty;
    }

    private void ClearAttackers()
    {
        attackers.Clear();
    }
}
