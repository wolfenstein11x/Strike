using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    const float wayPointRadius = 0.3f;

    private void OnDrawGizmos()
    {
        for (int i=0; i < transform.childCount; i++)
        {
            int j = GetNextIndex(i);

            Gizmos.DrawSphere(GetWaypoint(i), wayPointRadius);
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
        }
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }

    public int GetNextIndex(int i)
    {
        if (i < transform.childCount - 1)
        {
            return i + 1;
        }

        else return 0;
    }

    public int GetRandomWaypointIndex()
    {
        return Random.Range(0, transform.childCount);
    }
}
