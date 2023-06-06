using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EntityController : MonoBehaviour
{
    [SerializeField]
    private int[] manaAmountArray;

    [SerializeField]
    private int playerHealth;

    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    [SerializeField]
    private List<Card> playerHand = new List<Card>();

    private int _fatigueAmount = 1;

    public int[] ManaAmountArray
    {
        get { return manaAmountArray; }
    }

    public List<Card> PlayerCards
    {
        get { return playerCards; }
        set { playerCards = value; }
    }

    public List<Card> PlayerHand
    {
        get { return playerHand; }
    }

    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public int FatigueAmount
    {
        get { return _fatigueAmount; }
        set { _fatigueAmount = value; }
    }
}
