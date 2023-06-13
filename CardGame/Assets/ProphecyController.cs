using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProphecyController : Target
{
    [SerializeField]
    private Image prophecyIcon;

    [SerializeField]
    private TextMeshProUGUI prophecyProgressText;

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

        prophecyIcon.gameObject.SetActive(true);
        prophecyIcon.sprite = card.CardArt;
        prophecyProgressText.text = _prophecyProgress.ToString() + "/" + card.ProphecyMaxProgress;

        switch(_prophecyCard.ProphecyIncreaseEvent) //Subscribe to the event that will increase the prophecy's progress
        {
            case Card.EventType.DrawCard:
                GameplayManager.onDrawCard += IncreaseProphecyProgress;
                break;
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

        prophecyIcon.gameObject.SetActive(false);
        _prophecyCard = null;
    }
}
