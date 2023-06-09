using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetController TargetControllerRef
    {
        get { return this.GetComponent<TargetController>(); }
    }
}
