using UnityEngine;

namespace UI
{
    public class UiSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject switchToTheWindow;
        [SerializeField] private GameObject switchedWindow;

        public void OnClickSwitchWindow()
        {
            switchedWindow.SetActive(false);
            switchToTheWindow.SetActive(true);
        }
    }
}