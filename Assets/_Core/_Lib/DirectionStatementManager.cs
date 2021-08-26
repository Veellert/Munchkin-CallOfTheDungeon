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

public class DirectionStatementManager : MonoBehaviour
{
    [SerializeField] private GameObject _frontDirectionState;
    [SerializeField] private GameObject _backDirectionState;
    [SerializeField] private GameObject _leftDirectionState;
    [SerializeField] private GameObject _rightDirectionState;

    public eDirectionStatements CurrentDirection => (eDirectionStatements)_directionStates.FindIndex(s => s == _currentDirectionState);

    private GameObject _currentDirectionState;
    private List<GameObject> _directionStates;

    private void Start()
    {
        _directionStates = new List<GameObject>
        {
            _frontDirectionState, _backDirectionState, _leftDirectionState, _rightDirectionState,
        };

        if (_frontDirectionState)
            ChangeDirectionState(_frontDirectionState);
    }

    public void ChangeDirection(eDirectionStatements direction)
    {
        ChangeDirectionState(_directionStates[(int)direction]);
    }
    public void ChangeDirection(Vector2 direction)
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

    public GameObject GetCurrentDirectionStatement() => _currentDirectionState;

    private void ChangeDirectionState(GameObject directionState)
    {
        if (_currentDirectionState != null && _currentDirectionState == directionState)
            return;

        foreach (var state in _directionStates.FindAll(s => s != directionState))
            state.SetActive(false);

        _currentDirectionState = directionState;
        _currentDirectionState.SetActive(true);
    }
}

