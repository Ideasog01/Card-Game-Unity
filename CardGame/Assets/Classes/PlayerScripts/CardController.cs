using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private PlayerController playerController;

    private CardDisplayManager _cardDisplayManager;

    private Animator _cardAnimator;

    private Card _assignedCard;

    public Card AssignedCard
    {
        get { return _assignedCard; }
        set { _assignedCard = value; }
    }

    private void Start()
    {
        _cardAnimator = this.GetComponent<Animator>();
        _cardDisplayManager = GameObject.Find("GameManager").GetComponent<CardDisplayManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_assignedCard != null)
        {
            if(!_cardDisplayManager.cardSelectionObj.activeSelf)
            {
                _cardAnimator.SetBool("isHovered", true);
                playerController.SelectCard(this);
                _cardDisplayManager.DisplayCardSelectData(_assignedCard);
            }
        }
        else
        {
            Debug.LogWarning("ASSIGNED CARD IS NULL");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _cardAnimator.SetBool("isHovered", false);
        playerController.SelectCard(null);
    }
}
