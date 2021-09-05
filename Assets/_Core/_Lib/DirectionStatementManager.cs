using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDirectionStatements
{
    Front,
    Back,
    Left,
    Right,
}

public abstract class DirectionStatementManager : MonoBehaviour
{
    public eDirectionStatements CurrentDirection => (eDirectionStatements)_directionStates.FindIndex(s => s == _currentDirectionState);

    private GameObject _currentDirectionState;
    protected List<GameObject> _directionStates;

    public abstract void ChangeDirection(Vector2 direction);
    public void ChangeDirection(eDirectionStatements direction)
    {
        ChangeDirectionState(_directionStates[(int)direction]);
    }

    public GameObject GetCurrentDirectionStatement() => _currentDirectionState;

    protected void ChangeDirectionState(GameObject directionState)
    {
        if (_currentDirectionState != null && _currentDirectionState == directionState)
            return;

        foreach (var state in _directionStates.FindAll(s => s != directionState))
            state?.SetActive(false);

        _currentDirectionState = directionState;
        _currentDirectionState.SetActive(true);
    }
}
