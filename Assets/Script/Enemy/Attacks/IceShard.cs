using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : MonoBehaviour
{
    public List<BallLauncher> listOfShards = new List<BallLauncher>();
    public List<Transform> targets = new List<Transform>();

    public List<Transform> ActiveTargets = new List<Transform>();   


    public GameObject shards;

    public Transform visualParant;
    public Transform targetParant;

    public ParticleSystem shine;

    public int shardCount = 5;
    public bool isExplosionCompleted = false;

    [Header("Circuler Target")]
    public float radius; // Radius of the circle
    public float yOffset; // Offset along the y-axis
    private void Start()
    {
        SpwanMultiShards();
        StartCoroutine(LaunchIceShards());
    }
    public void SpwanMultiShards()
    {
        foreach (Transform item in visualParant)
        {
            item.gameObject.SetActive(false);
            listOfShards.Add(item.GetComponent<BallLauncher>());
        }
        foreach (Transform item in targetParant)
        {
            targets.Add(item);
        }
        PrepareToLaunce();
    }
    void ArrangeObjects(Transform target, int numberOfObjects, int i)
    {
        float angleStep = 360f / numberOfObjects;
        float angle = i * angleStep;
        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
        float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
        Vector3 pos = new Vector3(x, yOffset, z) + new Vector3(transform.position.x, 0, transform.position.z);
        target.position = pos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrepareToLaunce();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Launch();
        }
        if (isExplosionCompleted)
            Destroy(this.gameObject, .5f);
    }
    
    IEnumerator LaunchIceShards()
    {
        PrepareToLaunce();
        shine.Play();
        yield return new WaitForSeconds(5f);
        shine.Stop();
        StartCoroutine(NewLaunch());
        //Launch();
    }

    public void PrepareToLaunce()
    {
        ActiveTargets.Clear();
        for (int i = 0;i < shardCount; i++)
        {
            ArrangeObjects(targets[i], shardCount, i);
            targets[i].gameObject.SetActive(true);
            ActiveTargets.Add(targets[i]);
            listOfShards[i].target = targets[i];
        }
        // Get N of target ready
        // assign to list of object to be ready to shoot
        
    }
    public void ResetLaunchData()
    {
        foreach (Transform child in ActiveTargets)
        {
            child.GetComponent<ParticleSystem>().Stop();
        }
        ActiveTargets.Clear();
    }
    public void Launch()
    {
        for (int i = 0; i < shardCount; i++)
        {
            listOfShards[i].gameObject.SetActive(true);
            listOfShards[i].Launch();
        }
        Invoke("ResetLaunchData", .5f);
        // launch
    }

    IEnumerator NewLaunch()
    {
        for (int i = 0; i < shardCount; i++)
        {
            if (i >= shardCount-1)
            {
                listOfShards[i].isLastBall = true;
            }
            listOfShards[i].gameObject.SetActive(true);
            listOfShards[i].Launch();
            
            yield return new WaitForSeconds(.15f);
        }
        Invoke("ResetLaunchData", .5f);
    }

    public void CheckGroundDistance()
    {

    }
}
