using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class BallLauncher : MonoBehaviour {

	public IceShard parant;

	public Rigidbody ball;
	
	public BoxCollider boxCollider;
	public SphereCollider sphereCollider;

	public ParticleSystem energyBall;
	
	public GameObject ExplosionEffect;

	public Transform target;

	public float movementTime = 3;
	public float timeOffset = 1;
	public float h = 25;
	public float gravity = -18;

	public bool debugPath;
	public bool isLastBall = false;

	void Update() {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Launch ();
		}

		if (debugPath) {
			DrawPath ();
		}
	}

	public void Launch() {
		Physics.gravity = Vector3.up * gravity;
		ball.useGravity = true;
		ball.velocity = CalculateLaunchData ().initialVelocity;
	}

	LaunchData CalculateLaunchData() {
		float displacementY = target.position.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
		float time = Mathf.Sqrt(-movementTime * h / gravity) + Mathf.Sqrt(movementTime * (displacementY - h) / gravity);
		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;
		
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	public void Explode()
	{

        boxCollider.enabled = false;
        ball.isKinematic = true;
        StartCoroutine(ExplotionProcess());
		//Explosion script
	}


	IEnumerator ExplotionProcess()
	{
		yield return new WaitForSeconds(.15f);
        ExplosionEffect.SetActive(true);
        sphereCollider = transform.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        while (sphereCollider.radius < 22)
		{
			sphereCollider.radius += 50 * Time.deltaTime;			
			yield return null;
		}    
		if(sphereCollider.radius >= 21.9f)
		{
            
            yield return new WaitForSeconds(4f);
            CompleteExplosion();
        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(TagHash.GROUND))
		{
            energyBall.Stop();
            Invoke("Explode", timeOffset);
        }
    }

    void CompleteExplosion()
	{
        Destroy(sphereCollider);
		
        if (!parant.isExplosionCompleted && isLastBall)
        {
            parant.isExplosionCompleted = true;
        }
        StopCoroutine(ExplotionProcess());
    }

	void DrawPath() {
		LaunchData launchData = CalculateLaunchData ();
		Vector3 previousDrawPoint = ball.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++) {
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = ball.position + displacement;
			Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}
		
	}
}
	