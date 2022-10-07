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
        set { _assignedCard = value; }
    }

    private void Awake()
    {
        _cardAnimator = this.GetComponent<Animator>();
        _cardDisplayManager = playerController.GetComponent<CardDisplayManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_assignedCard != null)
        {
            _cardAnimator.SetBool("isHovered", true);
            playerController.SelectCard(this);
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
        playerController.SelectCard(null);
    }
}
