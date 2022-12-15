using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;
    private float volume = 1;

    public void Awake()
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        slider.value = volume;
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volume);
    }
}
