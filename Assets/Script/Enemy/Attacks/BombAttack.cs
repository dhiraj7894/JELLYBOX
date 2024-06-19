using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour
{
    public GameObject bombHitEffet;
    public float splashDuration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagHash.GROUND))
        {
            Destroy(gameObject);
            Destroy(Instantiate(bombHitEffet, transform.position, Quaternion.Euler(0, 0, 0)), splashDuration);
        }
    }
}
