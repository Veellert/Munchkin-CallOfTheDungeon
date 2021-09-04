using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideStatement : DirectionStatementManager
{
    [SerializeField] private GameObject _leftDirectionState;
    [SerializeField] private GameObject _rightDirectionState;

    private void Start()
    {
        _directionStates = new List<GameObject>
        {
            null, null, _leftDirectionState, _rightDirectionState,
        };

        if (_leftDirectionState)
            ChangeDirectionState(_leftDirectionState);
    }

    public override void ChangeDirection(Vector2 direction)
    {
        if (direction.x > 0)
            ChangeDirectionState(_rightDirectionState);
        else if (direction.x < 0)
            ChangeDirectionState(_leftDirectionState);
    }
}
