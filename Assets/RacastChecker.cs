using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacastChecker : MonoBehaviour
{
    public float Distance;

    void Update()
    {
        if(Physics.Raycast(transform.position, transform.position + transform.forward, out RaycastHit hit, Distance))
        {
            Debug.Log(hit.collider.name);
        }
        else
        {
            Debug.Log("NONE . . . ");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * Distance);
    }
}
