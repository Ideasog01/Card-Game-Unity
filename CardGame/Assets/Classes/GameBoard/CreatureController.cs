using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : EntityController
{
    [SerializeField]
    private GameObject creatureUI;

    [SerializeField]
    private SlotController slot;

    private int _creatureHealth;
    private int _creatureAttack;
    private Card _creatureCard;

    [Header("Creature Display")]

    [SerializeField]
    private Image creatureImage;

    [SerializeField]
    private TextMeshProUGUI creatureAttackText;

    [SerializeField]
    private TextMeshProUGUI creatureHealthText;

    [SerializeField]
    private Image creatureRangeImage;

    public Card CreatureCard
    {
        get { return _creatureCard; }
        set { _creatureCard = value; }
    }

    public SlotController AssignedSlot
    {
        get { return slot; }
    }

    public Transform CreatureUI
    {
        get { return creatureUI.transform; }
    }

    public int CreatureAttack
    {
        get { return _creatureAttack; }
    }

    private void Awake()
    {
        DisplayCreatureUI(false);

        DisplayDefaultParent = creatureUI.transform.parent;
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
        creatureImage.sprite = _creatureCard.CardArt;
        creatureAttackText.text = _creatureAttack.ToString();
        creatureHealthText.text = EntityHealth.ToString();
        creatureRangeImage.sprite = GameplayManager.gameDisplay.creatureReachIcons[(int)creatureCard.CreatureReach];

        AssignedPlayer = slot.AssignedPlayer;

        //GameplayManager.creatureControllerList.Add(_creatureController);
    }

    public void FightCreature(CreatureController other)
    {
        Debug.Log("FIGHT!\n" + _creatureCard.CardName + " attacks " + other.CreatureCard.CardName);

        int otherDamage = other.CreatureAttack;

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


    public void DisplayCreatureUI(bool active)
    {
        creatureImage.gameObject.SetActive(active);
        creatureAttackText.gameObject.SetActive(active);
        creatureHealthText.gameObject.SetActive(active);
        creatureRangeImage.gameObject.SetActive(active);
    }

    public void ChangeCreatureProperties(int attack)
    {
        _creatureAttack = attack;
        creatureAttackText.text = _creatureAttack.ToString();
    }
}
