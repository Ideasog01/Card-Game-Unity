using Photon.Pun.Demo.Cockpit.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static CardController cardSelected;

    [SerializeField]
    private GameObject cardSelectDisplay;

    [SerializeField]
    private List<Card> playerCards = new List<Card>();

    [SerializeField]
    private List<Card> playerHand = new List<Card>();

    private CardDisplayManager _cardDisplayManager;

    public List<Card> PlayerHand
    {
        get { return playerHand; }
    }

    public List<Card> PlayerCards
    {
        get { return playerCards; }
    }

    private void Awake()
    {
        _cardDisplayManager = this.GetComponent<CardDisplayManager>();
    }

    private void Start()
    {
        _cardDisplayManager.DisplayCardData(playerHand);
    }

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

    public void DrawCard()
    {

    }

    public void SortCards()
    {
        for (int i = 0; i < playerCards.Count; i++)
        {
            int rnd = Random.Range(0, playerCards.Count);
            Card tempCard = playerCards[rnd];
            playerCards[rnd] = playerCards[i];
            playerCards[i] = tempCard;
        }
    }
}
