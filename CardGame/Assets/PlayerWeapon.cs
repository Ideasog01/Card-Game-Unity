using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerWeapon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private EntityController entityController;

    private Card weaponCard;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(GameplayManager.playerIndex == 0 && weaponCard != null)
        {
            //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
            Debug.Log(name + " Game Object Clicked!");
        }
        
    }
}
