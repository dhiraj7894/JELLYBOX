using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacastChecker : MonoBehaviour
{
    public float Distance;
    public float Force=10;
    public LayerMask ignoreLayer;
    public bool isObjectUp = false;
    public bool isObjectDown = false;
    public Rigidbody rb;

    void Update()
    {
        if(Physics.Raycast(transform.position, transform.up, out RaycastHit hit , Distance, ignoreLayer))
        {
            if(hit.transform != null)
            {
                isObjectUp = true;                
            }
               
            else isObjectUp = false;

        }
        else if(Physics.Raycast(transform.position, -transform.up, out RaycastHit newHit, Distance, ignoreLayer))
        {
            if (newHit.transform != null)
            {
                isObjectDown = true;
            }

        }        
        else
        {
            isObjectUp = false;
            
            if (isObjectDown)
            {
                isObjectDown = false;
            }
            Debug.Log("NONE . . . ");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * Distance);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * Distance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * Distance);
    }
}
