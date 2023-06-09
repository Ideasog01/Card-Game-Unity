using UnityEngine;
using UnityEngine.UI;

public class StructureController : Target
{
    [SerializeField]
    private GameObject structureUI;

    [SerializeField]
    private SlotController slot;

    [SerializeField]
    private BoxCollider2D boxCollider;

    private Card _structureCard;

    //Structure

    [Header("Display")]

    [SerializeField]
    private Image structureImage;

    private Transform _displayDefaultParent;

    public Card StructureCard
    {
        get { return _structureCard; }
        set { _structureCard = value; }
    }

    public SlotController AssignedSlot
    {
        get { return slot; }
    }

    public BoxCollider2D BoxCollider
    {
        get { return boxCollider; }
    }

    public Transform StructureUI
    {
        get { return structureUI.transform; }
    }

    public Transform DisplayDefaultParent
    {
        get { return _displayDefaultParent; }
    }

    private void Awake()
    {
        _displayDefaultParent = structureUI.transform.parent;

        DisplayStructureUI(false);
    }

    public void AddStructure(Card structureCard)
    {
        DisplayStructureUI(true);

        _structureCard = structureCard;
        AssignStructureProperties(structureCard);
        structureImage.sprite = structureCard.CardArt;
    }

    public void AssignStructureProperties(Card structureCard)
    {
        _structureCard = structureCard;
    }

    public void DisplayStructureUI(bool active)
    {
        structureImage.gameObject.SetActive(active);
    }
}
