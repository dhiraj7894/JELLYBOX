
using UnityEngine;

public class DamagePlayerHP : MonoBehaviour
{
    public float HPLoss = 10;
    public void DestroySelfObject()
    {
        Destroy(this.gameObject);
    }
}
