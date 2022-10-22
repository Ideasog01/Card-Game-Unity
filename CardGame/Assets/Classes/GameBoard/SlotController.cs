using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class SlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject creatureUI;

    [SerializeField]
    private Card addTestCreature;

    [SerializeField]
    private Card testManaCard;

    private CreatureController _creatureController;

    private Image _creatureImage;

    private TextMeshProUGUI _creatureAttackText;

    private TextMeshProUGUI _creatureHealthText;

    private SpriteRenderer _slotRenderer;

    private Card _manaCard;

    private Card _creatureCard;

    private Card _structureCard;

    private Card _enchantmentCard;

    #region Properties

    public Card ManaCard
    {
        get { return _manaCard; }
    }

    public Card CreatureCard
    {
        get { return _creatureCard; }
        set { _creatureCard = value; }
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

    #endregion

    private void Awake()
    {
        _slotRenderer = this.GetComponent<SpriteRenderer>();
        _creatureController = creatureUI.GetComponent<CreatureController>();
        AssignCreatureUI();
        creatureUI.SetActive(false);
    }

    private void Start()
    {
        if(addTestCreature != null)
        {
            AddMana(testManaCard);
            AddCreature(addTestCreature, GameObject.Find("Player2").GetComponent<PlayerController>());
        }
    }

    public void AddMana(Card card)
    {
        _manaCard = card;
        _slotRenderer.sprite = card.CardArt;
        Debug.Log("Mana Added");
    }

    public void AddCreature(Card creatureCard, PlayerController player)
    {
        _creatureCard = creatureCard;
        _creatureController.AssignCreatureProperties(creatureCard, player, this);
        creatureUI.SetActive(true);
        _creatureImage.sprite = _creatureCard.CardArt;
        _creatureAttackText.text = _creatureCard.CreatureAttack.ToString();
        _creatureHealthText.text = _creatureCard.CreatureHealth.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerController.selectedSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerController.selectedSlot = null;
    }

    private void AssignCreatureUI()
    {
        _creatureImage = creatureUI.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        _creatureAttackText = creatureUI.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _creatureHealthText = creatureUI.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
    }
}
