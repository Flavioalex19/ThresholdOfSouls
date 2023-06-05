using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Perception : MonoBehaviour
{
    [SerializeField] float _viewAngle = 90f;
    [SerializeField] float _viewDistance = 10f;
    [SerializeField] LayerMask _targetMask;


    private void Update()
    {
    }

    private void OnDrawGizmos()
    {

        Vector3 rightDir = Quaternion.Euler(0f, _viewAngle / 2f, 0f) * transform.forward;
        Vector3 leftDir = Quaternion.Euler(0f, -_viewAngle / 2f, 0f) * transform.forward;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * _viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * _viewDistance);
    }

    public Transform DetectTarget()
    {
        Collider[] targetsInView = Physics.OverlapSphere(transform.position, _viewDistance, _targetMask);

        foreach (Collider targetCollider in targetsInView)
        {
            Transform target = targetCollider.transform;
            print(target.name);
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < _viewAngle / 2f)
            {
                return target;
            }
        }

        return null;
        
    }
}
