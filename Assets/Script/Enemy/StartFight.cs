using Jelly;
using Jelly.Core;
using Jelly.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartFight : IActionTrigger
{
    public MainEnemy MainEnemy;
    public PlayableDirector timeline;

    public void StartFightCutScene()
    {
        //GameManager.Instance.SetCutScene("LookAtEnemy");
        timeline.Play();
        StartCoroutine(StopTimeline((float)timeline.duration));
        MainEnemy.enabled = true;
        MainEnemy.anim.enabled = true;
        GetComponent<Collider>().enabled = false;
    }
    public override void Trigger()
    {
        Debug.Log("Boss Executed");
        StartFightCutScene();
    }


    IEnumerator StopTimeline(float time)
    {
        yield return new WaitForSeconds(time);
        timeline.gameObject.SetActive(false);
    }
}
