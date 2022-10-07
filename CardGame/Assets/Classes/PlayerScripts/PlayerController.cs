using Photon.Pun.Demo.Cockpit.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    private CardDisplayManager _cardDisplayManager;

    private void Awake()
    {
        _cardDisplayManager = this.GetComponent<CardDisplayManager>();
    }

    private void Start()
    {
        _cardDisplayManager.DisplayCardData(playerCards);
    }

    public List<Card> PlayerCards
    {
        get { return playerCards; }
    }

    public static CardController cardSelected;

    [SerializeField]
    private GameObject cardSelectDisplay;

    private void Update()
    {
        if (cardSelected != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cardSelectDisplay.SetActive(true);
                cardSelected.gameObject.SetActive(false);
            }

            if (cardSelectDisplay.activeSelf)
            {
                cardSelectDisplay.transform.position = Input.mousePosition;

                if (Input.GetMouseButtonUp(0))
                {
                    cardSelectDisplay.SetActive(false);
                    cardSelected.gameObject.SetActive(true);
                    cardSelected = null;
                }
            }
        }
    }

    public void SelectCard(CardController selectedCard)
    {
        if (!cardSelectDisplay.activeSelf)
        {
            cardSelected = selectedCard;
        }
    }
}
