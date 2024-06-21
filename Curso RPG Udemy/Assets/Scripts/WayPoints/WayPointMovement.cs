using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    Horizontal,
    Vertical
}

public class WayPointMovement : MonoBehaviour
{
   
    [SerializeField] protected float speed;

    public Vector3 PositionToMove => WayPoint.ObtainPosMovement(actualIndexPoint);

    protected WayPoint WayPoint;
    protected Animator _animator;
    protected int actualIndexPoint;
    protected Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        actualIndexPoint = 0;
        WayPoint = GetComponent<WayPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        RotateVertical();
        if(CheckActualPointAchieved())
        {
            UpdateIndexPoint();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, PositionToMove, speed * Time.deltaTime);
    }

    private bool CheckActualPointAchieved()
    {
        float distanceToActualPoint = (transform.position - PositionToMove).magnitude;

        if(distanceToActualPoint < 0.1f)
        {
            lastPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateIndexPoint()
    {
        if(actualIndexPoint == WayPoint.Points.Length - 1)
        {
            actualIndexPoint = 0;
        }
        else
        {
            if (actualIndexPoint < WayPoint.Points.Length - 1)
            {
                actualIndexPoint++;
            }
        }
    }

    protected virtual void Rotate()
    {
        
    }

    protected virtual void RotateVertical()
    {

    }
}
