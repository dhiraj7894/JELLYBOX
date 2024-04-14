using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAttack : MonoBehaviour
{
    public bool isHammerAttackComplete = false;
    public void GetHammerInfo()
    {
        isHammerAttackComplete = true;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
