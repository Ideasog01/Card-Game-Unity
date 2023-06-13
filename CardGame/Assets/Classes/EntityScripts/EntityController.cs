using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.iOS;

public class EntityController : Target
{
    [SerializeField]
    private int[] manaAmountArray;

    [SerializeField]
    private int entityHealth;

    [SerializeField]
    private UnityEvent onEntityTakeDamageEvent;

    [SerializeField]
    private UnityEvent onEntityDeathEvent;

    [SerializeField]
    private UnityEvent onEntityHealEvent;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [Header("Display")]

    [SerializeField]
    private Transform _displayDefaultParent;

    private int _fatigueAmount = 1;

    private bool _isPlayerDead;

    private int _maxEntityHealth;

    public int[] ManaAmountArray
    {
        get { return manaAmountArray; }
    }

    public int EntityHealth
    {
        get { return entityHealth; }
        set { entityHealth = value; }
    }

    public int EntityMaxHealth
    {
        get { return _maxEntityHealth; }
        set { _maxEntityHealth = value; }
    }

    public int FatigueAmount
    {
        get { return _fatigueAmount; }
        set { _fatigueAmount = value; }
    }

    public BoxCollider2D BoxCollider
    {
        get { return boxCollider; }
    }

    public Transform DisplayDefaultParent
    {
        get { return _displayDefaultParent; }
        set { _displayDefaultParent = value; }
    }

    public void TakeDamage(int amount, Target attacker)
    {
        if(!_isPlayerDead)
        {
            entityHealth -= amount;
            onEntityTakeDamageEvent.Invoke();

            if (entityHealth <= 0)
            {
                PlayerDeath();
            }

            if(attacker != null)
            {
                if (attacker.GetComponent<EntityController>() != null)
                {
                    GameplayManager.lastEntityAttacker = attacker.GetComponent<EntityController>();
                }
            }
            
            GameplayManager.onTakeDamage();
        }
    }

    public void Heal(int amount)
    {
        if (!_isPlayerDead)
        {
            entityHealth += amount;

            if(entityHealth > _maxEntityHealth)
            {
                entityHealth = _maxEntityHealth;
            }

            onEntityHealEvent.Invoke();

            if (entityHealth <= 0)
            {
                PlayerDeath();
            }
        }
    }

    private void PlayerDeath()
    {
        onEntityDeathEvent.Invoke();
        _isPlayerDead = true;
    }
}
