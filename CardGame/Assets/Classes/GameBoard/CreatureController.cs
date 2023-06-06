using UnityEngine;
using UnityEngine.EventSystems;

public class CreatureController : MonoBehaviour
{
    private int _creatureHealth;
    private int _creatureAttack;
    private Card _creatureCard;
    private SlotController _slot;

    public Card CreatureCard
    {
        get { return _creatureCard; }
        set { _creatureCard = value; }
    }

    public SlotController AssignedSlot
    {
        get { return _slot; }
    }

    public void AssignCreatureProperties(Card creatureCard, EntityController player, SlotController slot)
    {
        _creatureCard = creatureCard;
        _creatureHealth = creatureCard.CreatureHealth;
        _creatureAttack = creatureCard.CreatureAttack;
        _slot = slot;
    }

    public void FightCreature(CreatureController other)
    {
        Debug.Log("FIGHT!\n" + _creatureCard.CardName + " attacks " + other.CreatureCard.CardName);

        int otherDamage = other.CreatureCard.CreatureAttack;

        other.DamageCreature(_creatureAttack);
        DamageCreature(otherDamage);
    }

    public void DamageCreature(int amount)
    {
        _creatureHealth -= amount;

        if(_creatureHealth <= 0)
        {
            _creatureCard = null;
            _creatureHealth = 0;
            _creatureAttack = 0;
            _slot.AssignedCreatureController.CreatureCard = null;
            this.gameObject.SetActive(false);
        }
    }
}
