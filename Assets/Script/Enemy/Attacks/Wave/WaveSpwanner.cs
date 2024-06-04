using UnityEngine;


namespace Jelly.Enemy
{
    public class WaveSpwanner : MonoBehaviour
    {
        public GameObject Wave;
        private void Start()
        {
            LeanTween.scale(this.gameObject, new Vector3(1.5f, 1.5f, 1.5f), .5f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            Instantiate(Wave, new Vector3(transform.position.x, transform.position.y - transform.position.y, transform.position.z), Quaternion.identity);

            LeanTween.scale(this.gameObject, new Vector3(0, 0, 0), .5f).setOnComplete(() => {
            Destroy(this.gameObject);
            });
            GetComponent<Collider>().enabled = false;
            
        }
    }
}

