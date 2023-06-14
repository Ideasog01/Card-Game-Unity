using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "ScriptableObjects/CardEffect")]
public class CardEffect : ScriptableObject
{
    public enum ActivationType { None, OnStartTurn, OnEndTurn, OnDeath, OnCardPlayed };

    [SerializeField]
    private string effectDescription;

    [SerializeField]
    private Sprite effectIcon;

    [SerializeField]
    private ActivationType activationType;

    [SerializeField]
    private int effectDuration; //The number of turns the effect lasts for

    [SerializeField]
    private int effectValue; //The value that this effect uses, for example, healing or damage etc.

    [SerializeField]
    private bool infiniteDuration;

    public string EffectDescription
    {
        get { return effectDescription; }
    }

    public Sprite EffectIcon
    {
        get { return effectIcon; }
    }

    public ActivationType ActivationTypeRef
    {
        get { return activationType; }
    }

    public int EffectDuration
    {
        get { return effectDuration; }
    }

    public int EffectValue
    {
        get { return effectValue; }
    }

    public bool InfiniteDuration
    {
        get { return infiniteDuration; }
    }
}
