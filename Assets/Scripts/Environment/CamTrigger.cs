﻿using UnityEngine;
using UnityEngine.Events;

namespace Environment
{
    public class CamTrigger : MonoBehaviour
    {
        [SerializeField] private string camName;
        [SerializeField] private float cutsceneTime;
        
        public UnityEvent <string, float> onCamTriggerEnter;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            onCamTriggerEnter.Invoke(camName, cutsceneTime);
            gameObject.SetActive(false);
        }
    }
}