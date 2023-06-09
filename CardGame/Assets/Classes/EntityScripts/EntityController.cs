using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityController : MonoBehaviour
{
    [SerializeField]
    private int[] manaAmountArray;

    [SerializeField]
    private int entityHealth;

    [SerializeField]
    private UnityEvent onEntityTakeDamageEvent;

    [SerializeField]
    private UnityEvent onEntityDeathEvent;

    [Header("Display")]

    [SerializeField]
    private Transform _displayDefaultParent;

    private int _fatigueAmount = 1;

    private bool _isPlayerDead;

    public int[] ManaAmountArray
    {
        get { return manaAmountArray; }
    }

    public int EntityHealth
    {
        get { return entityHealth; }
        set { entityHealth = value; }
    }

    public int FatigueAmount
    {
        get { return _fatigueAmount; }
        set { _fatigueAmount = value; }
    }

    public Transform DisplayDefaultParent
    {
        get { return _displayDefaultParent; }
        set { _displayDefaultParent = value; }
    }

    public void TakeDamage(int amount)
    {
        if(!_isPlayerDead)
        {
            entityHealth -= amount;
            onEntityTakeDamageEvent.Invoke();

            Debug.Log("Player Took Damage!");

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
