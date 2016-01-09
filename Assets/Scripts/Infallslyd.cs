using UnityEngine;
using System.Collections;

public class Infallslyd : MonoBehaviour {

    public GameObject gameControl;
    public AudioClip infall;
    public AudioClip positivt_utfall;
    public AudioClip negativt_utfall;
    public AudioClip cloneMachine;
    public AudioClip buttonClick;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaySound(string str)
    {
        if (str == "innfall")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = infall;
            audio.Play();
        }
        if (str == "positivt")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = positivt_utfall;
            audio.Play();
        }
        if (str == "negativt")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = negativt_utfall;
            audio.Play();
        }
        if (str == "cloneMachine")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = cloneMachine;
            audio.Play();
        }
    }

    public void PlayOnClick()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = buttonClick;
        audio.Play();
    }
}
