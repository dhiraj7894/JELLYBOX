using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector2 movement;
    [Range(0, 1)] public float turnSmoothDamp;
    public NetworkBool isGrounded;
    
}
