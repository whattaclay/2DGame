using TMPro;
using UnityEngine;

namespace UI
{
    public class StartContinueButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI buttonText;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("ButtonText"))
            {
                buttonText.text = PlayerPrefs.GetString("ButtonText");
            }
        }
        public void SetButtonText(string text)
        {
            PlayerPrefs.SetString("ButtonText", text);
        }
    }
}