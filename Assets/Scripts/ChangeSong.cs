using UnityEngine;
using System.Collections;

public class ChangeSong : MonoBehaviour {

    public AudioClip song1;
    public AudioClip song2;

    bool playingSong2 = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("p") && playingSong2 == false)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = song2;
            audio.Play();
            audio.loop = true;
            playingSong2 = true;
        }
        if (Input.GetKeyDown("p") && playingSong2 == true)
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = song1;
            audio.Play();
            audio.loop = true;
            playingSong2 = false;
        }
    }
}
