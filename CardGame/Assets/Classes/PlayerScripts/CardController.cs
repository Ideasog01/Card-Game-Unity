using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardDisplayManager _cardDisplayManager;

    private Animator _cardAnimator;

    private Card _assignedCard;

    public Card AssignedCard
    {
        get { return _assignedCard; }
        set { _assignedCard = value; }
    }

    private void Awake()
    {
        _cardAnimator = this.GetComponent<Animator>();
        _cardDisplayManager = GameObject.Find("GameManager").GetComponent<CardDisplayManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_assignedCard != null)
        {
            _cardAnimator.SetBool("isHovered", true);
            _cardDisplayManager.LocalPlayer.SelectCard(this);
            _cardDisplayManager.DisplayCardSelectData(_assignedCard);
        }
        else
        {
            Debug.LogWarning("ASSIGNED CARD IS NULL");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardAnimator.SetBool("isHovered", false);
        _cardDisplayManager.LocalPlayer.SelectCard(null);
    }
}
