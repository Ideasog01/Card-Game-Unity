using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private SpriteRenderer creatureRenderer;

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
    }

    public Card StructureCard
    {
        get { return _structureCard; }
    }

    public Card EnchantmentCard
    {
        get { return _enchantmentCard; }
    }

    #endregion

    private void Awake()
    {
        _slotRenderer = this.GetComponent<SpriteRenderer>();
        creatureRenderer.enabled = false;
    }

    public void AddMana(Card card)
    {
        _manaCard = card;
        _slotRenderer.sprite = card.CardArt;
        Debug.Log("Mana Added");
    }

    public void AddCreature(Card creatureCard)
    {
        _creatureCard = creatureCard;
        creatureRenderer.enabled = true;
        creatureRenderer.sprite = _creatureCard.CardBattlefieldArt;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerController.selectedSlot = this;
        Debug.Log("ENTER... THE SLOT");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerController.selectedSlot = null;
    }
}
