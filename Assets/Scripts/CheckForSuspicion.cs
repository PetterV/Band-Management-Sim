using UnityEngine;
using System.Collections;

public class CheckForSuspicion : MonoBehaviour {

	void OnTriggerStay (Collider coll){
		if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<PlayerInteractions>().suspiciousAction == true){
			GetComponentInParent<BandMember>().SuspicionIncrease();
		}
	}
}
