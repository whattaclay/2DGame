﻿using TMPro;
using UnityEngine;

namespace UI
{
    public class MinusHealthText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;

        public void SetText(string text)
        {
            healthText.text = text;
        }
    }
}