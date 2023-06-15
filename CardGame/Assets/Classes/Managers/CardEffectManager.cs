using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardEffectManager : MonoBehaviour
{
    [SerializeField]
    public Transform cardEffectDisplayPrefab;

    //Card Effects
    public CardEffect divineShield;

    public void CardEffect(CardEffect cardEffect, Target target)
    {
        CreatureController creature = target.TargetControllerRef.CreatureControlllerRef;

        if (cardEffect.name == "DealTwoDamage")
        {
            target.GetComponent<EntityController>().TakeDamage(cardEffect.EffectValue, null);
        }

        if (cardEffect.name == "DealOneDamage")
        {
            target.GetComponent<EntityController>().TakeDamage(cardEffect.EffectValue, null);
        }

        if (cardEffect.name == "Mending")
        {
            target.GetComponent<EntityController>().Heal(cardEffect.EffectValue);
        }

        if(cardEffect.name == "GainMana")
        {
            GameUtilities.AddMana(target.GetComponent<EntityController>(), 0, cardEffect.EffectValue);
        }

        if(cardEffect.name == "SmiteEffect")
        {
            

            if(creature != null)
            {
                if(creature.CreatureTagList.Contains("Infernal") || creature.CreatureTagList.Contains("Undead"))
                {
                    creature.TakeDamage(creature.EntityMaxHealth, GameplayManager.activePlayer);
                }
                else
                {
                    creature.TakeDamage(4, GameplayManager.activePlayer);
                }
            }
            else
            {
                creature.TakeDamage(4, GameplayManager.activePlayer);
            }
        }

        if(cardEffect.name == "DivineShield")
        {
            if(creature != null)
            {
                creature.AddEffect(divineShield);
            }

            Debug.Log("Divine Shield!");
        }
    }
}

public class ActiveEffect
{
    private int _remainingActivations;

    private CardEffect _activeEffect;

    private Image _effectIcon;

    private TextMeshProUGUI _effectDurationText;

    public ActiveEffect(CardEffect effect, GameObject effectDisplay)
    {
        _remainingActivations = effect.EffectDuration;
        _activeEffect = effect;
        _effectIcon = effectDisplay.GetComponent<Image>();
        _effectDurationText = effectDisplay.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public int RemainingActivations
    {
        get { return _remainingActivations; }
        set { _remainingActivations = value; }
    }

    public CardEffect Effect
    {
        get { return _activeEffect; }
        set { _activeEffect = value; }
    }

    public Image EffectIcon
    {
        get { return _effectIcon; }
    }

    public TextMeshProUGUI EffectDurationText
    {
        get { return _effectDurationText; }
    }
}
