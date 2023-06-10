using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Card.TargetType targetType;

    [SerializeField]
    private EntityController _assignedPlayer;

    private TargetController _targetController;

    public TargetController TargetControllerRef
    {
        get { return _targetController; }
    }

    public Card.TargetType TargetType
    {
        get { return targetType; }
    }

    public EntityController AssignedPlayer
    {
        get { return _assignedPlayer; }
        set { _assignedPlayer = value; }
    }

    private void OnEnable()
    {
        _targetController = this.GetComponent<TargetController>();
    }
}
