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
	public int bringingSkill;
	public int bringingMedgjørlighet;
	public bool carryingGenMat = false;

	
	// Update is called once per frame
	void Update () {
		//Går nedover lista i prioritert rekkefølge for å interacte 
		if (Input.GetKeyUp("e")){
			if(bandCollision == true && currentBandMember.GetComponent<BandMember>().fikkKjeft == false && currentBandMember.GetComponent<BandMember>().active == true && currentBandMember.GetComponent<BandMember>().dead == false){
				print ("Don't do that!");
				currentBandMember.GetComponent<BandMember>().Kjeft();
			}
			else if(bandCollision == true && currentBandMember.GetComponent<BandMember>().dead == true && currentBandMember.GetComponent<BandMember>().beingCarried == false ){
				print ("Carrying dead body");
				currentBandMember.GetComponent<BandMember>().beingCarried = true;
			}
			else if (bandCollision == true && currentBandMember.GetComponent<BandMember>().beingCarried == true){
				print ("No longer carrying dead body");
				currentBandMember.GetComponent<BandMember>().beingCarried = false;
			}
			else if(bandCollision == true && currentBandMember.GetComponent<BandMember>().active == false){
				currentBandMember.GetComponent<BandMember>().active = true;
				print ("I'm active now!");
			}
			else if (genMatCollision == true){
				if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == false){
				print ("Yuck!");
				currentGenMat.GetComponent<GenetiskMateriale>().beingCarried = true;
				bringingRole = currentGenMat.GetComponent<GenetiskMateriale>().roleForCloning;
				bringingSkill = currentGenMat.GetComponent<GenetiskMateriale>().skillForCloning;
				bringingMedgjørlighet = currentGenMat.GetComponent<GenetiskMateriale>().medgjørlighetForCloning;
				carryingGenMat = true;
				}
				else if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == true && cloneMachineCollision == true){
					//print ("Destroy me - GenMat");
					GameObject.Find("CloneMachine").GetComponent<CloneMachine>().Cloning();
					Destroy(currentGenMat);
					carryingGenMat = false;
				}
				else if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == true){
					currentGenMat.GetComponent<GenetiskMateriale>().beingCarried = false;
					print ("I'm not carrying this!");
					carryingGenMat = false;
				}
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

	//Var lagd for BandMember
	void OnTriggerStay(Collider coll){
		if (coll.gameObject.tag == "BandMember" && Input.GetKeyDown("space")){
			if (currentBandMember.GetComponent<BandMember>().dead == false){
				currentBandMember.GetComponent<Innfallsystemet>().Interrupt();
				currentBandMember.GetComponent<BandMember>().Dying();
				GetComponent<AudioSource>().Play();
			}
		}
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
