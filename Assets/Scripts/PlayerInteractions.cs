using UnityEngine;
using System.Collections;

public class PlayerInteractions : MonoBehaviour {

	public bool bandCollision = false;
	public bool genMatCollision = false;
	public bool cloneMachineCollision = false;
	public bool happinessCollision = false;
	public bool computerCollision = false;
	public GameObject currentBandMember;
	public GameObject currentGenMat;
	public GameObject currentCloneMachine;
	public GameObject currentHappinessObject;
	public GameObject computer;
	public GameObject computerCanvas;
	public BandMember.Role bringingRole;
	public float bringingSkill;
	public int bringingMedgjørlighet;
	public bool carryingGenMat = false;
	public bool carryingHappiness = false;
	public bool carryingBody = false;
	public bool carryingAny = false;
	public bool computerActive = false;
	public int happinessToGive;
	public bool suspiciousAction = false;

	void Start (){
		computer = GameObject.Find("Computer");
		computerCanvas = GameObject.FindGameObjectWithTag("ComputerCanvas");
	}
	// Update is called once per frame
	void Update () {


		if (carryingGenMat == true || carryingBody == true || carryingHappiness == true){
			carryingAny = true;
		}
		if (carryingGenMat == false && carryingBody == false && carryingHappiness == false){
			carryingAny = false;
		}

		if (carryingBody == true){
			suspiciousAction = true;
		}
		if (carryingBody == false){
			suspiciousAction = false;
		}

		//Gi kjeft med Q
		if (Input.GetKeyUp("q")){
			if(bandCollision == true && currentBandMember.GetComponent<BandMember>().fikkKjeft == false && currentBandMember.GetComponent<CloneActivation>().active == true && currentBandMember.GetComponent<BandMember>().dead == false){
				print ("Don't do that!");
				currentBandMember.GetComponent<BandMember>().Kjeft();
			}
		}
		//Går nedover lista i prioritert rekkefølge for å interacte 
		if (Input.GetKeyDown("e")){
			if (bandCollision == true && happinessCollision == false){
				if(currentBandMember.GetComponent<BandMember>().dead == true && currentBandMember.GetComponent<BandMember>().beingCarried == false && carryingAny == false ){
					print ("Carrying dead body");
					currentBandMember.GetComponent<BandMember>().beingCarried = true;
					carryingBody = true;
				}
				else if (currentBandMember.GetComponent<BandMember>().beingCarried == true){
					print ("No longer carrying dead body");
					currentBandMember.GetComponent<BandMember>().beingCarried = false;
					carryingBody = false;
				}
				else if(currentBandMember.GetComponent<CloneActivation>().active == false){
					currentBandMember.GetComponent<CloneActivation>().active = true;
					print ("I'm active now!");
				}
			}
			else if (happinessCollision == true){
				if (carryingAny == false){
					print ("This will make someone happy");
					happinessToGive = currentHappinessObject.GetComponent<HappinessObject>().happinessGained;
					carryingHappiness = true;
					currentHappinessObject.GetComponent<HappinessObject>().happinessBeingCarried = true;
				}
				else if (carryingHappiness == true && bandCollision == false){
					print ("Who needs to make anyone happy?");
					currentHappinessObject.GetComponent<HappinessObject>().happinessBeingCarried = false;
					carryingHappiness = false;
				}
				else if (carryingHappiness == true && bandCollision == true){
					currentBandMember.GetComponent<BandMember>().HappinessGain();
					carryingHappiness = false;
					Destroy(currentHappinessObject);
				}
			}
			else if (genMatCollision == true){
				if (currentGenMat.GetComponent<GenetiskMateriale>().beingCarried == false && carryingBody == false && carryingHappiness == false && carryingGenMat == false){
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
			else if (computerCollision == true){
				if (computerActive == false){
					print ("Use computer");
					computerActive = true;
				}
				else if (computerActive == true){
					print ("No longer using computer");
					computerActive = false;
				}
			}
		}
		if (computerActive == true){
			computerCanvas.SetActive(true);
		}
		if (computerActive == false){
			computerCanvas.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider coll){
		print ("Hanging out with" + coll.gameObject.tag);
	}

	//Var lagd for BandMember, er her nå. Håper ingenting har blitt ødelagt.
	void OnTriggerStay(Collider coll){
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
		if (coll.gameObject.tag == "HappinessObject"){
			happinessCollision = true;
			currentHappinessObject = coll.gameObject;
		}
		if (coll.gameObject.tag == "Computer"){
			computerCollision = true;
		}

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
		if (coll.gameObject.tag == "HappinessObject"){
			happinessCollision = false;
		}
		if (coll.gameObject.tag == "Computer"){
			computerCollision = false;
			computerActive = false;
		}
		print ("No longer hanging out with" + coll.gameObject.tag);
	}
}
