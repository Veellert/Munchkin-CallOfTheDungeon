using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDirectionStatements
{
    Left,
    Right,
}

public class DirectionStatementManager : MonoBehaviour
{
    [SerializeField] private GameObject _leftDirection;
    [SerializeField] private GameObject _rightDirection;

    [HideInInspector] public eDirectionStatements eCurrentDirection
    {
        get
        {
            return _leftDirection?.name == CurrentDirection?.name ? eDirectionStatements.Left : eDirectionStatements.Right;
        }
    }
    [HideInInspector] public GameObject CurrentDirection { get; private set; }

    private void Start()
    {
        if (_rightDirection)
            ChangeDirectionState(_rightDirection);
    }

    public void ChangeDirection(Vector2 direction)
    {
        if (direction.x > 0)
            ChangeDirectionState(_rightDirection);
        else if (direction.x < 0)
            ChangeDirectionState(_leftDirection);
    }

    public void ChangeDirection(eDirectionStatements direction)
    {
        if(direction == eDirectionStatements.Left)
            ChangeDirectionState(_leftDirection);
        else
            ChangeDirectionState(_rightDirection);
    }

    private void ChangeDirectionState(GameObject directionState)
    {
        if (CurrentDirection && directionState && CurrentDirection == directionState)
            return;

        if (directionState.name == _leftDirection.name)
            _rightDirection?.SetActive(false);
        else
            _leftDirection?.SetActive(false);

        CurrentDirection = directionState;
        CurrentDirection?.SetActive(true);
    }
}
