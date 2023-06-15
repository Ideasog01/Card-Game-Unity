using System.Collections.Generic;
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
    private List<string> _creatureTagList = new List<string>();

    [Header("Creature Display")]

    [SerializeField]
    private Image creatureImage;

    [SerializeField]
    private TextMeshProUGUI creatureAttackText;

    [SerializeField]
    private TextMeshProUGUI creatureHealthText;

    [SerializeField]
    private Image creatureRangeImage;

    [SerializeField]
    private Transform cardEffectParent;

    private List<ActiveEffect> _activeEffects = new List<ActiveEffect>();

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

    public List<string> CreatureTagList
    {
        get { return _creatureTagList; }
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
        EntityMaxHealth = creatureCard.CreatureHealth;
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

        foreach(string tag in creatureCard.CardTags)
        {
            _creatureTagList.Add(tag);
        }

        AssignedPlayer = slot.AssignedPlayer;

        ApplyEffects();

        if(GameplayManager.onPlayCreature != null)
        {
            GameplayManager.onPlayCreature();
        }
    }

    public void FightCreature(CreatureController other)
    {
        Debug.Log("FIGHT!\n" + _creatureCard.CardName + " attacks " + other.CreatureCard.CardName);

        int otherDamage = other.CreatureAttack;

        other.TakeDamage(_creatureAttack, this);
        TakeDamage(otherDamage, other);
    }

    public void OnCreatureDeath()
    {
        _creatureCard = null;
        EntityHealth = 0;
        _creatureAttack = 0;
        _creatureCard = null;
        _creatureTagList.Clear();
        DisplayCreatureUI(false);
    }


    public void DisplayCreatureUI(bool active)
    {
        creatureImage.gameObject.SetActive(active);
        creatureAttackText.gameObject.SetActive(active);
        creatureHealthText.gameObject.SetActive(active);
        creatureRangeImage.gameObject.SetActive(active);
    }

    public void UpdateCreatureUI()
    {
        creatureHealthText.text = EntityHealth.ToString();
        creatureAttackText.text = _creatureAttack.ToString();
    }

    public void ChangeCreatureProperties(int attack)
    {
        _creatureAttack = attack;
        creatureAttackText.text = _creatureAttack.ToString();
    }

    public void DisplayOvertimeEffects()
    {
        foreach(ActiveEffect effect in _activeEffects)
        {
            if(effect.RemainingActivations == 0)
            {
                effect.EffectIcon.gameObject.SetActive(false);
            }
            else
            {
                effect.EffectIcon.gameObject.SetActive(true);
                effect.EffectIcon.sprite = effect.Effect.EffectIcon;
                effect.EffectDurationText.text = effect.RemainingActivations.ToString("F0");
            }
        }
    }

    public void TurnEffect(CardEffect.ActivationType activationType)
    {
        foreach(ActiveEffect effect in _activeEffects)
        {
            if(effect.Effect.ActivationTypeRef == activationType && effect.RemainingActivations > 0)
            {
                GameplayManager.cardEffectManager.CardEffect(effect.Effect, this);
                effect.RemainingActivations--;
            }
        }

        DisplayOvertimeEffects();
    }

    private void ApplyEffects()
    {
        foreach (CardEffect effect in _creatureCard.CardEffectList)
        {
            AddEffect(effect);
        }
    }

    public void AddEffect(CardEffect effect)
    {
        if(HasEffect(effect))
        {
            return;
        }

        ActiveEffect activeEffect = null;

        foreach (ActiveEffect active in _activeEffects) //Check to see if there is an effect object in the list that is no longer being used. If so, use it!
        {
            if (active.RemainingActivations == 0)
            {
                activeEffect = active;
                activeEffect.Effect = effect;
                activeEffect.RemainingActivations = effect.EffectDuration;

                break;
            }
        }

        if (activeEffect == null)
        {
            GameObject display = Instantiate(GameplayManager.cardEffectManager.cardEffectDisplayPrefab.gameObject, Vector3.zero, Quaternion.identity);
            display.transform.SetParent(cardEffectParent);
            activeEffect = new ActiveEffect(effect, display);
            _activeEffects.Add(activeEffect);
        }

        DisplayOvertimeEffects();
    }

    public void RemoveEffect(CardEffect effect)
    {
        foreach(ActiveEffect active in _activeEffects)
        {
            if(active.Effect == effect)
            {
                active.RemainingActivations = 0;
            }
        }

        DisplayOvertimeEffects();
    }

    public bool HasEffect(CardEffect effect)
    {
        foreach (ActiveEffect active in _activeEffects)
        {
            if (active.Effect == effect && active.RemainingActivations > 0)
            {
                return true;
            }
        }

        return false;
    }
}