using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SetVolumeSliderValue : MonoBehaviour
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            if (!PlayerPrefs.HasKey("VolumeValue")) return;
            _slider.value = PlayerPrefs.GetFloat("VolumeValue");
        }
    }
}