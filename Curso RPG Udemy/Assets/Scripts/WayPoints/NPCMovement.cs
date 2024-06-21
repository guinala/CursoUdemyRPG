using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : WayPointMovement
{
    [SerializeField] private Direction direction;
    private readonly int walkDown = Animator.StringToHash("Down");

    protected override void Rotate()
    {
        if (direction != Direction.Horizontal)
        {
            return;
        }

        if (PositionToMove.x > lastPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void RotateVertical()
    {
        if(direction != Direction.Vertical)
        {
            return;
        }

        if(PositionToMove.y > lastPosition.y)
        {
            _animator.SetBool(walkDown, false);
        }
        else
        {
            _animator.SetBool(walkDown, true);
        }
    }
}
