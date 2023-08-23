using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Environment;
using UnityEngine;

//Manager мусорное слово, не несет смысловой нагрузки
public class CamManager : MonoBehaviour
{
    [SerializeField] private GameObject baseCamera;
    [SerializeField] private List<GameObject> cameras;
    public CamTrigger[] triggers;

    private void Awake()
    {
        triggers = GetComponentsInChildren<CamTrigger>();
    }
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
    public void SwitchToCamera(string toCamera)
    {
        foreach (var t in cameras.Where(t => t.name == toCamera))
        {
            t.SetActive(true);
            baseCamera.SetActive(false);
        }
    }
    public void BackToBaseCamera()
    {
        foreach (var t in cameras.Where(t=> t.activeSelf == true))
        {
            StartCoroutine(BackToBaseCam(t, 0f));
        }
    }
    private IEnumerator BackToBaseCam(GameObject currentCam, float timeForCutscene)
    {
        yield return new WaitForSeconds(timeForCutscene);
        baseCamera.SetActive(true);
        currentCam.SetActive(false);
    }
}
