using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : Target
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    private Card _assignedWeapon;

    public Card AssignedWeapon
    {
        get { return _assignedWeapon; }
        set { _assignedWeapon = value; }
    }

    public BoxCollider2D BoxCollider
    {
        get { return boxCollider; }
    }
}
