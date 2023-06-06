using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotController : MonoBehaviour
{
    [SerializeField]
    private GameObject creatureUI;

    private CreatureController _creatureController;

    private Image _creatureImage;

    private TextMeshProUGUI _creatureAttackText;

    private TextMeshProUGUI _creatureHealthText;

    private SpriteRenderer _slotRenderer;

    private Card _manaCard;

    private Card _structureCard;

    private Card _enchantmentCard;

    private BoxCollider2D _boxCollider;

    private EntityController _assignedPlayer;

    #region Properties

    public Card ManaCard
    {
        get { return _manaCard; }
    }

    public Card StructureCard
    {
        get { return _structureCard; }
    }

    public Card EnchantmentCard
    {
        get { return _enchantmentCard; }
    }

    public CreatureController AssignedCreatureController
    {
        get { return _creatureController; }
    }

    public BoxCollider2D SlotBox
    {
        get { return _boxCollider; }
    }

    public EntityController AssignedPlayer
    {
        get { return _assignedPlayer; }
    }

    #endregion

    private void Awake()
    {
        _slotRenderer = this.GetComponent<SpriteRenderer>();
        _creatureController = creatureUI.GetComponent<CreatureController>();
        AssignCreatureUI();
        creatureUI.SetActive(false);

        _boxCollider = this.GetComponent<BoxCollider2D>();
    }

    public void AddMana(Card card, EntityController player)
    {
        _manaCard = card;
        _slotRenderer.sprite = card.CardArt;
        Debug.Log("Mana Added");
        _assignedPlayer = player;
    }

    public void AddCreature(Card creatureCard, EntityController player)
    {
        _creatureController.CreatureCard = creatureCard;
        _creatureController.AssignCreatureProperties(creatureCard, player, this);
        creatureUI.SetActive(true);
        _creatureImage.sprite = _creatureController.CreatureCard.CardArt;
        _creatureAttackText.text = _creatureController.CreatureCard.CreatureAttack.ToString();
        _creatureHealthText.text = _creatureController.CreatureCard.CreatureHealth.ToString();
    }

    private void AssignCreatureUI()
    {
        _creatureImage = creatureUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _creatureAttackText = creatureUI.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _creatureHealthText = creatureUI.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
    }
}
