using Jelly;
using Jelly.Core;
using Jelly.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFight : IActionTrigger
{
    public MainEnemy MainEnemy;


    public void StartFightCutScene()
    {
        GameManager.Instance.SetCutScene("LookAtEnemy");
        MainEnemy.enabled = true;
        GetComponent<Collider>().enabled = false;
    }
    public override void Trigger()
    {
        Debug.Log("Boss Executed");
        StartFightCutScene();
    }
}
