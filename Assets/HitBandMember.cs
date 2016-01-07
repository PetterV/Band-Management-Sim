using UnityEngine;
using System.Collections;

public class HitBandMember : MonoBehaviour {

	public GameObject player;

	void Start (){
		player = GameObject.FindWithTag("Player");
	}

	void OnTriggerEnter (Collider coll){
		if (coll.gameObject.tag == "BandMember"){
			player.GetComponent<PlayerInteractions>().isHitting = true;
			player.GetComponent<PlayerInteractions>().currentBandMember = coll.gameObject;
		}
	}
}
