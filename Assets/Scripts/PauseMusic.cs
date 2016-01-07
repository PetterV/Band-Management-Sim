using UnityEngine;
using System.Collections;

public class PauseMusic : MonoBehaviour {
    private float timer = 0f;

    // Use this for initialization
    void Start() {
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void Pause()
    {
        GetComponent<AudioSource>().Pause();
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        GetComponent<AudioSource>().Play();
    }
}
