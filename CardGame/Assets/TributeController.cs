using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TributeController : Target
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private PlayerHero playerHero;

    private bool _tributeActivated;

    public BoxCollider2D BoxCollider
    {
        get { return boxCollider; }
    }

    public bool TributeActivated
    {
        get { return _tributeActivated; }
    }

    private void Awake()
    {
        playerHero.TributeProgressText.text = playerHero.TributeProgress + "/" + playerHero.TributeMaxProgress;
        GameplayManager.onPlayCreature += IncreaseTributeProgress;
    }

    public void IncreaseTributeProgress()
    {
        if(GameplayManager.lastCardPlayed != null && GameplayManager.activePlayer == this.AssignedPlayer && !_tributeActivated)
        {
            playerHero.TributeProgress++;

            if(playerHero.TributeProgress >= playerHero.TributeMaxProgress)
            {
                ActivateTribute();
                _tributeActivated = true;
            }
        }

        playerHero.TributeProgressText.text = playerHero.TributeProgress + "/" + playerHero.TributeMaxProgress;
    }

    public void ActivateTribute()
    {
        if(GameplayManager.onTributeActivation != null)
        {
            GameplayManager.onTributeActivation();
            playerHero.HideTribute();
        }
    }
}
