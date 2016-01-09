using UnityEngine;
using System.Collections;

public class Infallslyd : MonoBehaviour {

    public GameObject gameControl;
    public AudioClip infall;
    public AudioClip positivt_utfall;
    public AudioClip negativt_utfall;


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
            print("lyd!");
        }
        if (str == "positivt")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = positivt_utfall;
            audio.Play();
            print("lyd!");
        }
        if (str == "negativt")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = negativt_utfall;
            audio.Play();
            print("lyd!");
        }
    }
}
