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

    public void DamageCreature(int amount)
    {
        _creatureHealth -= amount;

        if(_creatureHealth <= 0)
        {
            
        }
    }
}
