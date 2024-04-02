using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Transform lockOnTransform;
    public void DrawGizmos(float detectionRadius, Color32 col)
    {
        Gizmos.color = col;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
}
