using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPath : MonoBehaviour
{
    const float wayPointRadius = 0.3f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(GetWaypoint(0), wayPointRadius);
        Gizmos.DrawSphere(GetWaypoint(1), wayPointRadius);
        Gizmos.DrawLine(GetWaypoint(0), GetWaypoint(1));   
    }

    public Vector3 GetWaypoint(int i)
    {
        return transform.GetChild(i).position;
    }
}
