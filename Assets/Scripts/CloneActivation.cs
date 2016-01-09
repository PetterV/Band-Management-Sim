using UnityEngine;
using System.Collections;

public class CloneActivation : MonoBehaviour {

	public bool active = false;
	public GameObject startWalkTarget;

	void Start (){
		startWalkTarget = GameObject.Find("StartSted");
	}

	public void Activation() {
		if (GetComponent<BandMember>().dead == false){
			GetComponent<BandMember>().active = true;
		}
        Innfallsystemet infs = GetComponent<Innfallsystemet>();
        infs.enabled = true;
       // infs.harInnfall = true;
		GetComponent<BandMemberMoving>().enabled = true;
        
		GetComponent<BandMemberMoving>().waypointToMoveTo = startWalkTarget;
	}
}
