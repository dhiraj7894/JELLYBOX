
using UnityEngine;

public class DamagePlayerHP : MonoBehaviour
{
    public float damage = 10;
    public void DestroySelfObject()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagHash.PLAYER))
        {
            other.GetComponent<IHealthSystem>().TakeDamage(damage);
        }
    }
}
