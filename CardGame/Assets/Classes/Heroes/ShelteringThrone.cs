using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelteringThrone : MonoBehaviour
{
    [SerializeField]
    private PlayerEntityController playerEntity;

    private List<EntityController> attackers = new List<EntityController>();

    private void Awake()
    {
        GameplayManager.onTakeDamage += DivineRetribution;
        GameplayManager.onEndTurn += ClearAttackers;
    }

    public void DivineRetribution() //Deal damage to attacker (Only once)
    {
        if(GameplayManager.lastEntityAttacker != null)
        {
            if (!attackers.Contains(GameplayManager.lastEntityAttacker))
            {
                GameplayManager.lastEntityAttacker.TakeDamage(1, playerEntity);
                attackers.Add(GameplayManager.lastEntityAttacker);
            }
        }
    }

    private void ClearAttackers()
    {
        attackers.Clear();
    }
}
