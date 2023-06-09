using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Card.TargetType targetType;

    private TargetController _targetController;

    public TargetController TargetControllerRef
    {
        get { return _targetController; }
    }

    public Card.TargetType TargetType
    {
        get { return targetType; }
    }

    private void OnEnable()
    {
        _targetController = this.GetComponent<TargetController>();
    }
}
