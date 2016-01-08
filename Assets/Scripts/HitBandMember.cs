using UnityEngine;
using System.Collections;

public class HitBandMember : MonoBehaviour {

	public GameObject player;
	public float hitSoundDelay = 0.1f;

	void Start (){
		player = GameObject.FindWithTag("Player");
	}

	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.tag == "BandMember"){
			player.GetComponent<PlayerInteractions>().isHitting = true;
			player.GetComponent<PlayerInteractions>().bandMemberToKill = coll.gameObject;
			player.GetComponent<AudioSource>().PlayDelayed(hitSoundDelay);
		}
	}
}
