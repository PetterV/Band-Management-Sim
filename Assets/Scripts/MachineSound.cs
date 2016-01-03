using UnityEngine;
using System.Collections;

public class MachineSound : MonoBehaviour {

    public AudioClip audioClip;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && Input.GetKeyDown("space"))
        {
            GetComponent<AudioSource>().Play();

        }
    }
}
