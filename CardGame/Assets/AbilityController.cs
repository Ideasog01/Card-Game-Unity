using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : Target
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    public BoxCollider2D BoxCollider
    {
        get { return boxCollider; }
    }
}
