using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // TODO Slider
    public Slider volumeSlider;

    // private void Awake()
    // {
    // TODO Mute
    //     if (SoundsManager.Instance.music.mute == true)
    //     {
    //         toggle.isOn = false;
    //         Debug.Log("Status Music Mute:" + SoundsManager.Instance.music.mute);
    //     }
    //     else
    //     {
    //         toggle.isOn = true;
    //         Debug.Log("Status Music Mute :" + SoundsManager.Instance.music.mute);
    //     }
    // }

    private void Start()
    {
        // TODO Load Volume
        volumeSlider.value = SoundsManager.Instance.music.volume;
        SoundsManager.Instance.LoadVolume();
        Debug.Log("Volume Music : " + volumeSlider.value);
    }

    // TODO slider volume
    public void SliderVolume()
    {
        SoundsManager.Instance.music.volume = volumeSlider.value;
        Debug.Log("Volume Music : " + volumeSlider.value);
    }

    // TODO mute
    public Toggle toggle;
    public void SetMute()
    {
        if (toggle.isOn == true)
        {
            SoundsManager.Instance.music.mute = false;
            Debug.Log("Status Music Mute :" + SoundsManager.Instance.music.mute);
        }
        else
        {
            SoundsManager.Instance.music.mute = true;
            Debug.Log("Status Music Mute :" + SoundsManager.Instance.music.mute);
        }
    }

    // TODO keluar game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Keluar Aplikasi");
    }
}
