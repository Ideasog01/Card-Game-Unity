using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProphecyController : Target
{
    [SerializeField]
    private Image prophecyIcon;

    [SerializeField]
    private TextMeshProUGUI prophecyProgressText;

    [SerializeField]
    private TextMeshProUGUI prophecyTurnsText;

    private int _turnsRemaining;

    private Card _prophecyCard;

    private int _prophecyProgress = 0;

    public Card ProphecyCard
    {
        get { return _prophecyCard; }
    }

    private void Awake()
    {
        prophecyIcon.gameObject.SetActive(false);
    }

    public void AddProphecy(Card card)
    {
        _prophecyCard = card;
        _prophecyProgress = 0;
        _turnsRemaining = card.ProphecyMaxTurns;

        prophecyIcon.gameObject.SetActive(true);
        prophecyIcon.sprite = card.CardArt;
        prophecyProgressText.text = _prophecyProgress.ToString() + "/" + card.ProphecyMaxProgress;
        prophecyTurnsText.text = _turnsRemaining.ToString();

        switch (_prophecyCard.ProphecyIncreaseEvent) //Subscribe to the event that will increase the prophecy's progress
        {
            case Card.EventType.DrawCard:
                GameplayManager.onDrawCard += IncreaseProphecyProgress;
                break;
        }

        GameplayManager.onStartTurn += DecrementProphecyTurns;
    }

    private void RemoveProphecy()
    {
        prophecyIcon.gameObject.SetActive(false);
        _prophecyCard = null;
    }

    public void DecrementProphecyTurns()
    {
        if(AssignedPlayer == GameplayManager.activePlayer)
        {
            _turnsRemaining--;
            prophecyTurnsText.text = _turnsRemaining.ToString();

            if (_turnsRemaining <= 0)
            {
                RemoveProphecy();
                GameplayManager.onStartTurn -= DecrementProphecyTurns;
            }
        }
    }

    public void IncreaseProphecyProgress()
    {
        if(_prophecyCard != null && AssignedPlayer == GameplayManager.activePlayer)
        {
            _prophecyProgress++;
            prophecyProgressText.text = _prophecyProgress.ToString() + "/" + _prophecyCard.ProphecyMaxProgress;

            if (_prophecyProgress >= _prophecyCard.ProphecyMaxProgress)
            {
                ProphecyComplete();
                GameplayManager.onDrawCard -= IncreaseProphecyProgress;
            }
        }
    }

    private void ProphecyComplete()
    {
        foreach(CardEffect effect in _prophecyCard.CardEffectList)
        {
            GameplayManager.cardEffectManager.CardEffect(effect, AssignedPlayer);
            Debug.Log("Prophecy effect activated.");
        }

        RemoveProphecy();
    }
}
