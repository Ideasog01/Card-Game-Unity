using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantmentController : Target
{
    [SerializeField]
    private Image enchantmentIcon;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private Transform displayDefaultParent;

    private Card _enchantmentCard;

    public BoxCollider2D BoxCollider
    {
        get { return boxCollider; }
    }

    public Transform DisplayDefaultParent
    {
        get { return displayDefaultParent; }
    }

    public Card EnchantmentCard
    {
        get { return _enchantmentCard; }
    }

    public Transform EnchantmentUI
    {
        get { return enchantmentIcon.transform; }
    }

    private void Awake()
    {
        DisplayEnchantmentUI(false);
        displayDefaultParent = enchantmentIcon.transform.parent;
    }

    public void AddEnchantment(Card enchantmentCard)
    {
        _enchantmentCard = enchantmentCard;
        DisplayEnchantmentUI(true);
    }

    private void DisplayEnchantmentUI(bool active)
    {
        enchantmentIcon.gameObject.SetActive(active);

        if(active)
        {
            enchantmentIcon.sprite = _enchantmentCard.CardArt;
        }
    }
}
