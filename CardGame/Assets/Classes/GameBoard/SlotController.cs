using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotController : MonoBehaviour
{
    [SerializeField]
    private GameObject slotUI;

    //Creature

    private CreatureController _creatureController;

    private Image _creatureImage;

    private TextMeshProUGUI _creatureAttackText;

    private TextMeshProUGUI _creatureHealthText;

    private Image _creatureRangeImage;

    //General

    private SpriteRenderer _manaArt;

    private SpriteRenderer _slotBorder;

    private Card _manaCard;

    private Card _structureCard;

    private Card _enchantmentCard;

    private BoxCollider2D _boxCollider;

    private EntityController _assignedPlayer;

    private Transform _defaultParent;

    //Structure

    private StructureController _structureController;

    private Image _structureImage;

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

    public StructureController AssignedStructureController
    {
        get { return _structureController; }
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

        _creatureController = slotUI.GetComponent<CreatureController>();
        AssignCreatureUI();
        DisplayCreatureUI(false);

        _structureController = slotUI.GetComponent<StructureController>();
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

    public void AddCreature(Card creatureCard)
    {
        DisplayCreatureUI(true);

        _creatureController.CreatureCard = creatureCard;
        _creatureController.AssignCreatureProperties(creatureCard, this);
        _creatureImage.sprite = _creatureController.CreatureCard.CardArt;
        _creatureAttackText.text = _creatureController.CreatureCard.CreatureAttack.ToString();
        _creatureHealthText.text = _creatureController.CreatureCard.CreatureHealth.ToString();
        _creatureRangeImage.sprite = GameplayManager.gameDisplay.creatureReachIcons[(int)creatureCard.CreatureReach];

        GameplayManager.creatureControllerList.Add(_creatureController);
    }

    private void AssignCreatureUI()
    {
        _creatureImage = slotUI.transform.GetChild(0).GetComponent<Image>();
        _creatureAttackText = slotUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _creatureHealthText = slotUI.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        _creatureRangeImage = slotUI.transform.GetChild(0).GetChild(2).GetComponent<Image>();
    }

    public void AddStructure(Card structureCard)
    {
        DisplayStructureUI(true);

        _structureController.StructureCard = structureCard;
        _structureController.AssignStructureProperties(structureCard, this);
        _structureImage.sprite = _structureController.StructureCard.CardArt;

        GameplayManager.structureControllerList.Add(_structureController);
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
