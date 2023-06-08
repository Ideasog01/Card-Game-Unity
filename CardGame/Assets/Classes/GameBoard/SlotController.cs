using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotController : EntityController
{
    [SerializeField]
    private GameObject slotUI;

    //Creature

    private Image _creatureImage;

    private TextMeshProUGUI _creatureAttackText;

    private TextMeshProUGUI _creatureHealthText;

    private Image _creatureRangeImage;

    private int _creatureAttack;

    private Card _creatureCard;

    //General

    private SpriteRenderer _manaArt;

    private SpriteRenderer _slotBorder;

    private Card _manaCard;

    private Card _enchantmentCard;

    private BoxCollider2D _boxCollider;

    private EntityController _assignedPlayer;

    private Transform _defaultParent;

    //Structure

    private Image _structureImage;

    private Card _structureCard;

    #region Properties

    public Card ManaCard
    {
        get { return _manaCard; }
    }

    public Card CreatureCard
    {
        get { return _creatureCard; }
    }

    public Card StructureCard
    {
        get { return _structureCard; }
    }

    public Card EnchantmentCard
    {
        get { return _enchantmentCard; }
    }

    public BoxCollider2D SlotBox
    {
        get { return _boxCollider; }
    }

    public EntityController AssignedPlayer
    {
        get { return _assignedPlayer; }
    }

    public Transform DefaultParent
    {
        get { return _defaultParent; }
    }

    #endregion

    private void Awake()
    {
        _defaultParent = this.transform.parent;

        _slotBorder = this.GetComponent<SpriteRenderer>();

        _manaArt = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        _manaArt.gameObject.SetActive(false);

        AssignCreatureUI();
        DisplayCreatureUI(false);

        AssignStructureUI();
        DisplayStructureUI(false);

        _boxCollider = this.GetComponent<BoxCollider2D>();
    }

    public void AddMana(Card card, EntityController player)
    {
        _manaCard = card;

        _manaArt.gameObject.SetActive(true);
        _manaArt.sprite = card.CardArt;

        if(player == GameplayManager.humanPlayer) //Set the border. Blue for player, Red for Enemy
        {
            _slotBorder.color = GameplayManager.gameDisplay.humanColour;
        }
        else
        {
            _slotBorder.color = GameplayManager.gameDisplay.enemyColour;
        }

        Debug.Log("Mana Added");
        _assignedPlayer = player;
    }

    public void AssignCreatureProperties(Card creatureCard)
    {
        _creatureCard = creatureCard;
        EntityHealth = creatureCard.CreatureHealth;
        _creatureAttack = creatureCard.CreatureAttack;
    }

    public void AddCreature(Card creatureCard)
    {
        DisplayCreatureUI(true);

        AssignCreatureProperties(creatureCard);
        _creatureImage.sprite = _creatureCard.CardArt;
        _creatureAttackText.text = _creatureAttack.ToString();
        _creatureHealthText.text = EntityHealth.ToString();
        _creatureRangeImage.sprite = GameplayManager.gameDisplay.creatureReachIcons[(int)creatureCard.CreatureReach];

        //GameplayManager.creatureControllerList.Add(_creatureController);
    }

    private void AssignCreatureUI()
    {
        _creatureImage = slotUI.transform.GetChild(0).GetComponent<Image>();
        _creatureAttackText = slotUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _creatureHealthText = slotUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        _creatureRangeImage = slotUI.transform.GetChild(0).GetChild(2).GetComponent<Image>();
    }

    public void FightCreature(SlotController other)
    {
        Debug.Log("FIGHT!\n" + _creatureCard.CardName + " attacks " + other.CreatureCard.CardName);

        int otherDamage = other.CreatureCard.CreatureAttack;

        other.TakeDamage(_creatureAttack);
        TakeDamage(otherDamage);
    }

    public void OnCreatureDeath()
    {
        _creatureCard = null;
        EntityHealth = 0;
        _creatureAttack = 0;
        _creatureCard = null;
        DisplayCreatureUI(false);
    }

    public void AddStructure(Card structureCard)
    {
        DisplayStructureUI(true);

        _structureCard = structureCard;
        AssignStructureProperties(structureCard);
        _structureImage.sprite = structureCard.CardArt;
    }

    public void AssignStructureProperties(Card structureCard)
    {
        _structureCard = structureCard;
    }

    private void AssignStructureUI()
    {
        _structureImage = slotUI.transform.GetChild(1).GetComponent<Image>();
    }

    public void DisplayCreatureUI(bool active)
    {
        _creatureImage.gameObject.SetActive(active);
        _creatureAttackText.gameObject.SetActive(active);
        _creatureHealthText.gameObject.SetActive(active);
        _creatureRangeImage.gameObject.SetActive(active);
    }

    public void DisplayStructureUI(bool active)
    {
        _structureImage.gameObject.SetActive(active);
    }
}
