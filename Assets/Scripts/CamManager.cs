using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] private GameObject baseCamera;
    [SerializeField] private List<GameObject> cameras;

    public void ChangeCamera(string cameraName, float timeForCutscene)
    {
        foreach (var t in cameras.Where(t => t.name == cameraName))
        {
            t.SetActive(true);
            baseCamera.SetActive(false);
            StartCoroutine(BackToBaseCam(t,timeForCutscene));
            return;
        }
    }
    private IEnumerator BackToBaseCam(GameObject currentCam, float timeForCutscene)
    {
        yield return new WaitForSeconds(timeForCutscene);
        baseCamera.SetActive(true);
        currentCam.SetActive(false);
    }
}
