using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public bool bandCollision = false;
	public bool genMatCollision = false;
	public bool cloneMachineCollision = false;
	public GameObject currentBandMember;
	public GameObject currentGenMat;
	public GameObject currentCloneMachine;

	
	// Update is called once per frame
	void Update () {
		//Går nedover lista i prioritert rekkefølge for å interacte 
		if (Input.GetKeyUp("e")){
			if(bandCollision == true){
				print ("Don't do that!");
				currentBandMember.GetComponent<BandMember>().Kjeft();
			}
			else if (genMatCollision == true){
				print ("Yuck!");
			}
			else if (cloneMachineCollision == true){
				print ("Let's get this show on the road.");
			}
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "BandMember"){
			bandCollision = true;
		}
		if (coll.gameObject.tag == "GeneticMaterial"){
			genMatCollision = true;
		}
		if (coll.gameObject.tag == "CloneMachine"){
			cloneMachineCollision = true;
		}
		print ("Hanging out with" + coll.gameObject.tag);
	}

	void OnTriggerExit (Collider coll){
		if (coll.gameObject.tag == "BandMember"){
			bandCollision = false;
			currentBandMember = coll.gameObject;
		}
		if (coll.gameObject.tag == "GeneticMaterial"){
			genMatCollision = false;
			currentGenMat = coll.gameObject;
		}
		if (coll.gameObject.tag == "CloneMachine"){
			cloneMachineCollision = false;
			currentCloneMachine = coll.gameObject;
		}
		print ("No longer hanging out with" + coll.gameObject.tag);
	}
}
