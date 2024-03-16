using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera Camera;
    private void Start()
    {
        Camera = Camera.main;
    }

    public void LookAtCam()
    {
        //transform.LookAt(Camera.main.transform.position);
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
    }
}
