using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundToggle : MonoBehaviour {


    private AudioSource musicAudio;
    private AudioSource soundAudio;
    public GameObject soundToggle;
    public GameObject musicToggle;

    private Toggle soundToggleState;
    private Toggle musicToggleState;
          
    void Start()
    {
        //instantiation
        musicAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        soundAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        soundToggleState = soundToggle.GetComponent<Toggle>();
        musicToggleState = musicToggle.GetComponent<Toggle>();

        musicToggleState.isOn = SoundController.isMusicOn;
        soundToggleState.isOn = SoundController.isSoundOn;
    }

    public void toggleMusic (bool toggle) {
        musicAudio.mute = !toggle;
        SoundController.isMusicOn = toggle;
        print("isSoundMute is" + toggle);
    }

    public void toggleSound(bool toggle)
    {
        soundAudio.mute = !toggle;
        SoundController.isSoundOn = toggle;
        print("isSoundMute is" + toggle);
    }

}
