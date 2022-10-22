using UnityEngine;
using UnityEngine.EventSystems;

public class CreatureController : MonoBehaviour
{
    private int _creatureHealth;
    private int _creatureAttack;
    private Card _creatureCard;
    private SlotController _slot;

    private PlayerController _assignedPlayer;

    public PlayerController AssignedPlayer
    {
        get { return _assignedPlayer; }
    }

    public Card CreatureCard
    {
        get { return _creatureCard; }
    }

    public void AssignCreatureProperties(Card creatureCard, PlayerController player, SlotController slot)
    {
        _creatureCard = creatureCard;
        _creatureHealth = creatureCard.CreatureHealth;
        _creatureAttack = creatureCard.CreatureAttack;
        _assignedPlayer = player;
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
            _assignedPlayer = null;
            _creatureHealth = 0;
            _creatureAttack = 0;
            _slot.CreatureCard = null;
            this.gameObject.SetActive(false);
        }
    }
}
