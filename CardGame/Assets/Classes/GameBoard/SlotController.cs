using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotController : MonoBehaviour
{
    //General

    [SerializeField]
    private Image manaArt;

    [SerializeField]
    private Transform defaultParent;

    [SerializeField]
    private Image slotBorder;

    private Card _manaCard;

    private Card _enchantmentCard;

    private EntityController _assignedPlayer;

    #region Properties

    public Card ManaCard
    {
        get { return _manaCard; }
    }

    public Card EnchantmentCard
    {
        get { return _enchantmentCard; }
    }

    public EntityController AssignedPlayer
    {
        get { return _assignedPlayer; }
    }

    public Image SlotBorder
    {
        get { return slotBorder; }
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

        Debug.Log("Mana Added");
        _assignedPlayer = player;
    }
}
