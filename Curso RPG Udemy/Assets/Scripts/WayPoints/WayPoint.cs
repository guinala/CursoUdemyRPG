using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;

    public Vector3 pos { get; set; }
    bool inPlayMode = false;

    private void Start()
    {
        inPlayMode = true;
        pos = transform.position;
    }

    public Vector3 ObtainPosMovement(int index)
    {
        return points[index] + pos;
    }

    private void OnDrawGizmos()
    {
        if(!inPlayMode && transform.hasChanged)
        {
            pos = transform.position;
        }

        if(points == null || points.Length <= 0)
        {
            return;
        }

        for(int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(points[i] + pos, 0.5f);

            if(i < points.Length - 1)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(points[i] + pos, points[i + 1] + pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
