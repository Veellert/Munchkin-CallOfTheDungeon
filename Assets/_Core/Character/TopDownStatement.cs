using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownStatement : DirectionStatementManager
{
    [SerializeField] private GameObject _frontDirectionState;
    [SerializeField] private GameObject _backDirectionState;
    [SerializeField] private GameObject _leftDirectionState;
    [SerializeField] private GameObject _rightDirectionState;

    private void Start()
    {
        _directionStates = new List<GameObject>
        {
            _frontDirectionState, _backDirectionState, _leftDirectionState, _rightDirectionState,
        };

        if (_frontDirectionState)
            ChangeDirectionState(_frontDirectionState);
    }

    public override void ChangeDirection(Vector2 direction)
    {
        if (direction.y > 0)
            ChangeDirectionState(_backDirectionState);
        else if (direction.y < 0)
            ChangeDirectionState(_frontDirectionState);

        if (direction.x > 0)
            ChangeDirectionState(_rightDirectionState);
        else if (direction.x < 0)
            ChangeDirectionState(_leftDirectionState);
    }

}
