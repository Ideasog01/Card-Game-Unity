using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotController : Target
{
    //General

    [SerializeField]
    private Image manaArt;

    [SerializeField]
    private Transform defaultParent;

    [SerializeField]
    private Image slotBorder;

    [SerializeField]
    private BoxCollider2D boxCollider;

    private Card _manaCard;

    private Card _enchantmentCard;

    #region Properties

    public Card ManaCard
    {
        get { return _manaCard; }
    }

    public Card EnchantmentCard
    {
        get { return _enchantmentCard; }
    }

    public Image SlotBorder
    {
        get { return slotBorder; }
    }

    public BoxCollider2D BoxCollider
    {
        get { return boxCollider; }
    }

    public Transform DefaultParent
    {
        get { return defaultParent; }
    }

    #endregion

    private void Awake()
    {
        manaArt.gameObject.SetActive(false);
    }

    public void AddMana(Card card, EntityController player)
    {
        _manaCard = card;

        manaArt.gameObject.SetActive(true);
        manaArt.sprite = card.CardArt;

        if(player == GameplayManager.humanPlayer) //Set the border. Blue for player, Red for Enemy
        {
            slotBorder.color = GameplayManager.gameDisplay.humanColour;
        }
        else
        {
            slotBorder.color = GameplayManager.gameDisplay.enemyColour;
        }

        AssignedPlayer = player;
    }
}
