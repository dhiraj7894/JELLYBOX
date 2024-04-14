using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    public bool isCircleAttackComplete = false;

    public void GetAttackValue()
    {
        isCircleAttackComplete = true;

    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
