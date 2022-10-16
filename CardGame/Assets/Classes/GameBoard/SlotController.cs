using System.Net.NetworkInformation;
using UnityEngine;

public class SlotController : MonoBehaviour
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

    public void OnMouseOver()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(PlayerController.cardSelected != null)
            {
                AddMana(PlayerController.cardSelected.AssignedCard);
            }
        }
    }
}
