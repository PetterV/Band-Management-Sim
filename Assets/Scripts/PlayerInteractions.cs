using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public bool bandCollision = false;
	public bool genMatCollision = false;
	public bool cloneMachineCollision = false;
	public GameObject currentBandMember;
	public GameObject currentGenMat;
	public GameObject currentCloneMachine;
	public BandMember.Role bringingRole;
	public bool carryingGenMat = false;

	
	// Update is called once per frame
	void Update () {
		//Går nedover lista i prioritert rekkefølge for å interacte 
		if (Input.GetKeyUp("e")){
			if(bandCollision == true){
				print ("Don't do that!");
				currentBandMember.GetComponent<BandMember>().Kjeft();
			}
			else if (genMatCollision == true){
				if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == false){
				print ("Yuck!");
				currentGenMat.GetComponent<GenetiskMateriale>().beingCarried = true;
				bringingRole = currentGenMat.GetComponent<GenetiskMateriale>().roleForCloning;
				carryingGenMat = true;
				}
				else if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == true && cloneMachineCollision == true){
					print ("Destroy me - GenMat");
					Destroy(currentGenMat);
					carryingGenMat = false;
				}
				else if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == true){
					currentGenMat.GetComponent<GenetiskMateriale>().beingCarried = false;
					print ("I'm not carrying this!");
					carryingGenMat = false;
				}
			}
			else if (cloneMachineCollision == true){
				print ("Let's get this show on the road.");
			}
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "BandMember"){
			bandCollision = true;
			currentBandMember = coll.gameObject;
		}
		if (coll.gameObject.tag == "GeneticMaterial" && carryingGenMat == false){
			genMatCollision = true;
			currentGenMat = coll.gameObject;
		}
		if (coll.gameObject.tag == "CloneMachine"){
			cloneMachineCollision = true;
			currentCloneMachine = coll.gameObject;
		}
		print ("Hanging out with" + coll.gameObject.tag);
	}

	void OnTriggerExit (Collider coll){
		if (coll.gameObject.tag == "BandMember"){
			bandCollision = false;
		}
		if (coll.gameObject.tag == "GeneticMaterial" && carryingGenMat == false){
			genMatCollision = false;
		}
		if (coll.gameObject.tag == "CloneMachine"){
			cloneMachineCollision = false;
		}
		print ("No longer hanging out with" + coll.gameObject.tag);
	}
}
