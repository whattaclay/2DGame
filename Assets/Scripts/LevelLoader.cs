using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI percentageText;
    public void LoadLevel(int sceneIndex)
    {
        if (sceneIndex != 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            percentageText.text = (progress * 100f).ToString("F1") + "%";
            image.fillAmount = progress;
            yield return null;
        }
    }
}