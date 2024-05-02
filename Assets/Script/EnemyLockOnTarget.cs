using Jelly.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLockOnTarget : MonoBehaviour
{
    public Animator CinemachineAnimator;
    public MainPlayer mainPlayer;
    public GameObject currentCrosshair;
    private void Update()
    {
        AnimatorStateInfo stateInfo = CinemachineAnimator.GetCurrentAnimatorStateInfo(0);
        int stateHash = stateInfo.shortNameHash;

        if (InputActions._lockOn.triggered && mainPlayer.nearByEnemy.Length != 0)
        {
            if (stateHash == Animator.StringToHash(TagHash.PTARGET))
            {
                CinemachineAnimator.Play(TagHash.ETARGET);
                mainPlayer.LockTheTarget();
                currentCrosshair.SetActive(false);
            }
            else
            {
                CinemachineAnimator.Play(TagHash.PTARGET);
                mainPlayer.LockTheTarget();
                currentCrosshair.SetActive(true);
            }
        }
    }
}
