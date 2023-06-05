using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Drawing;

public class CardDisplayManager : MonoBehaviour
{
    public GameObject cardSelectionObj;

    [SerializeField]
    private List<GameObject> playerCards = new List<GameObject>();

    [SerializeField]
    private Image[] manaIcons;

    [SerializeField]
    private UnityEngine.Color[] manaColors;

    [SerializeField]
    private UnityEngine.Color disabledManaColor;

    [SerializeField]
    private PlayerController playerController;

    private List<CardDisplay> cardDisplayList = new List<CardDisplay>();

    private CardDisplay _cardSelectionDisplay;

    private void Awake()
    {
        _cardSelectionDisplay = new CardDisplay();
        _cardSelectionDisplay.AssignCardProperties(cardSelectionObj);

        foreach(GameObject card in playerCards)
        {
            CardDisplay cardDisplay = new CardDisplay();
            cardDisplay.AssignCardProperties(card);
            cardDisplayList.Add(cardDisplay);  
        }
    }

    public void DisplayMana()
    {
        int overallCount = 0;

        foreach(Image image in manaIcons)
        {
            image.color = disabledManaColor;
        }

        for(int m = 0; m < playerController.ManaAmountArray.Length; m++)
        {
            for (int i = 0; i < playerController.ManaAmountArray[m]; i++)
            {
                manaIcons[overallCount].color = manaColors[m];
                overallCount++;
            }
        }

        Debug.Log("Mana Displayed");
    }

    public void DisplayCardSelectData(Card card)
    {
        CardDisplay cardDisplay = _cardSelectionDisplay;
        cardDisplay.CardNameText.text = card.CardName;
        cardDisplay.CardDescriptionText.text = card.CardDescription;
        cardDisplay.CardArt.sprite = card.CardArt;
        cardDisplay.AttackText.text = card.CreatureAttack.ToString();
        cardDisplay.HealthText.text = card.CreatureHealth.ToString();
    }

    public void DisplayCardData(List<Card> cardList)
    {
        for(int i = 0; i < cardDisplayList.Count; i++)
        {
            if(i < cardList.Count)
            {
                cardDisplayList[i].CardObj.SetActive(true);
                cardDisplayList[i].CardObj.GetComponent<CardController>().AssignedCard = cardList[i];
            }
            else
            {
                cardDisplayList[i].CardObj.SetActive(false);
                //Debug.Log("CARD DISABLED");
            }
        }

        for(int x = 0; x < cardDisplayList.Count; x++)
        {
            if(x < cardList.Count)
            {
                Card card = cardList[x];
                CardDisplay cardDisplay = cardDisplayList[x];
                cardDisplay.CardNameText.text = card.CardName;
                cardDisplay.CardDescriptionText.text = card.CardDescription;
                cardDisplay.CardArt.sprite = card.CardArt;
                cardDisplay.AttackText.text = card.CreatureAttack.ToString();
                cardDisplay.HealthText.text = card.CreatureHealth.ToString();
                cardDisplay.CardObj.GetComponent<CardController>().AssignedCard = card;
                //Change Mana Display
            }
        }
    }
}

public struct CardDisplay
{
    private GameObject _cardObj;
    private Image _cardArt;
    private TextMeshProUGUI _cardNameText;
    private TextMeshProUGUI _cardDescriptionText;
    private TextMeshProUGUI _manaCostText1;
    private TextMeshProUGUI _manaCostText2;
    private Image _rangeIcon;
    private TextMeshProUGUI _cardTagText;
    private TextMeshProUGUI _attackText;
    private TextMeshProUGUI _healthText;
    
    public GameObject CardObj
    {
        get { return _cardObj; }
    }

    public Image CardArt
    {
        get { return _cardArt; }
    }

    public TextMeshProUGUI CardNameText
    {
        get { return _cardNameText; }
    }

    public TextMeshProUGUI CardDescriptionText
    {
        get { return _cardDescriptionText; }
    }

    public TextMeshProUGUI ManaCostText1
    {
        get { return _manaCostText1; }
    }

    public TextMeshProUGUI ManaCostText2
    {
        get { return _manaCostText2; }
    }

    public Image RangeIcon
    {
        get { return _rangeIcon; }
    }

    public TextMeshProUGUI CardTagText
    {
        get { return _cardTagText; }
    }

    public TextMeshProUGUI AttackText
    {
        get { return _attackText; }
    }

    public TextMeshProUGUI HealthText
    {
        get { return _healthText; }
    }

    public void AssignCardProperties(GameObject card)
    {
        _cardArt = card.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();

        _cardNameText = card.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        _cardDescriptionText = card.transform.GetChild(0).GetChild(0).GetChild(7).GetComponent<TextMeshProUGUI>();

        _manaCostText1 = card.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _manaCostText2 = card.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();

        _rangeIcon = card.transform.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>();

        _cardTagText = card.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>();

        _attackText = card.transform.GetChild(0).GetChild(0).GetChild(5).GetComponent<TextMeshProUGUI>();
        _healthText = card.transform.GetChild(0).GetChild(0).GetChild(6).GetComponent<TextMeshProUGUI>();

        _cardObj = card;
    }
}
