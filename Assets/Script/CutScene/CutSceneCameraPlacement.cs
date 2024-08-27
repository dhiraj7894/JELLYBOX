using Jelly.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCameraPlacement : MonoBehaviour
{
    public float cutSceneEndTime = 0.1f;
    public float cutSceneStartDelay = .1f;
    public string textForCutScene = "NONE";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagHash.PLAYER))
        {        
            GetComponent<Collider>().enabled = false;
            StartCameraCutScene();
        }
    }

    public void StartCameraCutScene()
    {
        GameManager.Instance.VirtualCamera.m_Follow = transform;
        
        GameManager.Instance.CutSceneStart();
        UIManager.Instance.cutSceneCameraTexts.text = textForCutScene;
        LeanTween.value(this.gameObject, 0, 1, cutSceneStartDelay).setOnUpdate((float val) =>
        {
            UIManager.Instance.cutSceneCamera.alpha = val;
            if (val >= 1)
            {
                //GameManager.Instance.SetCutScene(TagHash.CutScene);
                LeanTween.delayedCall(cutSceneEndTime, () =>
                {
                    GameManager.Instance.CutSceneEnd();
                    UIManager.Instance.cutSceneCamera.alpha = 0;
                    GameManager.Instance.VirtualCamera.m_Follow = null;
                    Destroy(gameObject);
                });
            }
        });
    }

    float val;
    IEnumerator LoadCutSceneTextArea(float time)
    {
        yield return new WaitForSeconds(time);
        while (val < 1)
        {
            //val += fadeSpeed * Time.deltaTime;
            UIManager.Instance.cutSceneCamera.alpha = val;
            if (val >= 1)
            {
                UIManager.Instance.cutSceneCamera.alpha = 1;
                LeanTween.delayedCall(cutSceneEndTime, () =>
                {
                    val = 0;
                    GameManager.Instance.CutSceneEnd();
                    UIManager.Instance.cutSceneCamera.alpha = val;
                });
                StopCoroutine(LoadCutSceneTextArea(0));
            }
        }

    }
}
