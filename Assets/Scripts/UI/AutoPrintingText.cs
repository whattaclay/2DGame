using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class AutoPrintingText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string finalText;
        [SerializeField] private float timeBetweenPrintingLetters = 0.1f;
        private void Awake()
        {
            text.text = "";
        }
        private IEnumerator PrintingText()
        {
            foreach (var letter in finalText)
            {
                text.text += letter;
                yield return new WaitForSeconds(timeBetweenPrintingLetters);
            }
        }
    }
}