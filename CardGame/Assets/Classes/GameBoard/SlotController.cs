using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private SpriteRenderer _slotRenderer;

    private Card _manaCard;

    private Card _creatureCard;

    private Card _structureCard;

    private Card _enchantmentCard;

    private void Awake()
    {
        _slotRenderer = this.GetComponent<SpriteRenderer>();
    }

    public void AddMana(Card card)
    {
        _slotRenderer.sprite = card.CardArt;
        Debug.Log("Mana Added");
    }

    public void AddCreature(Card creatureCard)
    {
        _creatureCard = creatureCard;
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
