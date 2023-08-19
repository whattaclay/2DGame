using System;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class CutScene : MonoBehaviour
    {
        public UnityEvent onDestroyCutScene;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("IsCutScenePlayed"))
            {
                Destroy(gameObject);
            }
        }
        private void OnDestroy()
        {
            PlayerPrefs.SetInt("IsCutScenePlayed",1);
            onDestroyCutScene.Invoke();
        }
    }
}