using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    private AudioSource audioSrc;

    
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = PlayerPrefs.GetFloat("volume", 0.1f);
        audioSrc.mute = PlayerPrefs.GetInt("mute", 0) == 0 ? true : false;

    }

    public void SetVolume(float vol)
    {
        PlayerPrefs.SetFloat("volume", vol/5);
    }

    public void ToggleMusic(bool mute)
    {
        PlayerPrefs.SetInt("mute", mute ? 1 : 0);
    }
}
