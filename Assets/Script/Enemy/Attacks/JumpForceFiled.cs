
using UnityEngine;

public class JumpForceFiled : MonoBehaviour
{
    public float HPLoss = 10;
    public void DestroySelfObject()
    {
        Destroy(this.gameObject);
    }
}
