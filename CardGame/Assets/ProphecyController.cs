using UnityEngine;
using UnityEngine.UI;

public class ProphecyController : Target
{
    [SerializeField]
    private Image prophecyIcon;

    private Card _prophecyCard;

    public Card ProphecyCard
    {
        get { return _prophecyCard; }
    }

    public void AddProphecy(Card card)
    {
        prophecyIcon.sprite = card.CardArt;
        _prophecyCard = card;
    }
}
