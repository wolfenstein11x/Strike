using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTesting : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 midsectionOffset;
    [SerializeField] float range = 25f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float angleRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        midsectionOffset = new Vector3(0f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
        ShootRay();
    }

    void ShootRay()
    {
        Vector3 startPoint = transform.position + midsectionOffset;
        Vector3 endPoint = target.transform.position + midsectionOffset;

        Vector3 dir = (endPoint - startPoint).normalized;

        RaycastHit hit;
        if (Physics.Raycast(startPoint, dir, out hit, range) && WithinAngle(dir))
        {
            Debug.DrawRay(startPoint, dir * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(startPoint, dir * range, Color.yellow);
        }

    }

    bool WithinAngle(Vector3 direction)
    {
        float angle = Vector3.Angle(direction, transform.forward);
        //Debug.Log(angle + " degrees");
        return angle <= angleRange;
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
