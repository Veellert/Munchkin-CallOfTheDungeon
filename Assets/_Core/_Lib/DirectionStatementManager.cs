using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eDirectionStatement
{
    Left,
    Right,
}

public class DirectionStatementManager : MonoBehaviour
{
    [SerializeField] private GameObject _model;

    public eDirectionStatement CurrentDirection { get; private set; }

    private Vector2 _scale;
    private Vector2 _scaleN;

    private void Start()
    {
        if (!_model)
            return;

        var scale = _model.transform.localScale;
        _scale = new Vector2(scale.x, scale.y);
        _scaleN = new Vector2(-scale.x, scale.y);

        ChangeDirectionState(eDirectionStatement.Right);
    }

    public void ChangeDirection(Vector2 direction)
    {
        if (direction.x > 0)
            ChangeDirectionState(eDirectionStatement.Right);
        else if (direction.x < 0)
            ChangeDirectionState(eDirectionStatement.Left);
    }

    public void ChangeDirection(eDirectionStatement direction)
    {
        ChangeDirectionState(direction);
    }

    private void ChangeDirectionState(eDirectionStatement direction)
    {
        if (CurrentDirection == direction || !_model)
            return;

        if (direction == eDirectionStatement.Left)
            _model.transform.localScale = _scaleN;
        else
            _model.transform.localScale = _scale;

        CurrentDirection = direction;
    }
}
