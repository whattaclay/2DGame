using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
        
    private void Start()
    {
        if (!PlayerPrefs.HasKey("VolumeValue")) return;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat("VolumeValue")) * 20);
    }
    public void SetVolume(Slider slider)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat("VolumeValue",slider.value);
    }
}